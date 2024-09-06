using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.AuthServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, object>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;

        public ResetUserPasswordCommandHandler(IApplicationDbContext applicationDbContext, IMediator mediator, IAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _mediator = mediator;
            _authService = authService;
        }

        public async Task<object> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {

                ResponseModel response = await _mediator.Send(new VerifyUserCommand
                {
                    Email = request.Email,
                    SentPassword = request.SentPassword
                });

                if(response.IsSuccess==false)
                {
                    return response;
                }

                User user = await _applicationDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == request.Email); 

                if(user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "User does not exist!"
                    };
                }

                UpdateUserCommand updateUserCommand = user.Adapt<UpdateUserCommand>();
                updateUserCommand.Email = request.Email;
                updateUserCommand.Password = request.NewPassword;

                response = await _mediator.Send(updateUserCommand);

                if(response.IsSuccess==false)
                {
                    return response;
                }

                _applicationDbContext.Verifications.Remove(await _applicationDbContext.Verifications.FirstOrDefaultAsync(v => v.Email == request.Email));
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return _authService.GenerateToken(await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email));
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
