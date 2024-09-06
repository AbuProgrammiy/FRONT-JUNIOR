using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.AuthServices;
using FrontJunior.Application.Services.PasswordServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class LogUserInCommandHandler : IRequestHandler<LogUserInCommand, object>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IPasswordService _passwordService;
        private readonly IAuthService _authService;

        public LogUserInCommandHandler(IApplicationDbContext applicationDbContext, IPasswordService passwordService, IAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _passwordService = passwordService;
            _authService = authService;
        }

        public async Task<object> Handle(LogUserInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "User is not found!"
                    };
                }

                bool response = _passwordService.CheckPassword(request.Password, new PasswordModel
                {
                    PassworSalt = user.PassworSalt,
                    PasswordHash = user.PasswordHash
                });

                if (response == false) 
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "Password is incorrect!"
                    };
                }

                return _authService.GenerateToken(user);
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
