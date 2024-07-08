using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.AuthServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
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
                Verification verification = await _applicationDbContext.Verifications.FirstOrDefaultAsync(v => v.Email == request.Email);

                if (verification == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Email not found!"
                    };
                }

                if(verification.SentPassword!=request.SentPassword)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "Password is incorrect!"
                    };
                }

                ResponseModel response = await _mediator.Send(new CreateUserCommand
                {
                    Email = request.Email,
                    Password = request.Password,
                });

                if(response.IsSuccess==false)
                {
                    return response;
                }

                _applicationDbContext.Verifications.Remove(verification);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return _authService.GenerateToken(await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email));
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = $"Something went wrong: {ex.Message}"
                };
            }
        }
    }
}
