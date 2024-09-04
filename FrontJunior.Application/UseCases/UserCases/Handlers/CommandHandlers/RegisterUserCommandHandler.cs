using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.AuthServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, object>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IAuthService _authService;
        private readonly IMediator _mediator;

        public RegisterUserCommandHandler(IApplicationDbContext applicationDbContext, IAuthService authService, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
            _mediator = mediator;
        }

        public async Task<object> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ResponseModel response = await _mediator.Send(new VerifyUserCommand
                {
                    Email = request.Email,
                    SentPassword = request.SentPassword,
                });

                if(response.IsSuccess==false)
                {
                    return response;
                }

                response = await _mediator.Send(new CreateUserCommand
                {
                    Email = request.Email,
                    Password = request.Password,
                });

                if(response.IsSuccess==false)
                {
                    return response;
                }

                _applicationDbContext.Verifications.Remove(await _applicationDbContext.Verifications.FirstOrDefaultAsync(v=>v.Email==request.Email));
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return _authService.GenerateToken(await _applicationDbContext.ActiveUsers.FirstOrDefaultAsync(u => u.Email == request.Email));
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Response = $"Something went wrong: {ex.Message}"
                };
            }
        }
    }
}
