using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.AuthServices;
using FrontJunior.Application.Services.PasswordServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, object>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;
        private readonly IPasswordService _passwordService;

        public ResetUserPasswordCommandHandler(IApplicationDbContext applicationDbContext, IMediator mediator, IAuthService authService, IPasswordService passwordService)
        {
            _applicationDbContext = applicationDbContext;
            _mediator = mediator;
            _authService = authService;
            _passwordService = passwordService;
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

                ActiveUser user = await _applicationDbContext.ActiveUsers.FirstOrDefaultAsync(u => u.Email == request.Email); 

                if(user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "User does not exist!"
                    };
                }

                PasswordModel passwordModel = _passwordService.HashPassword(request.NewPassword);

                user.PasswordHash= passwordModel.PasswordHash;
                user.PassworSalt= passwordModel.PassworSalt;

                _applicationDbContext.Verifications.Remove(await _applicationDbContext.Verifications.FirstOrDefaultAsync(v => v.Email == request.Email));
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
