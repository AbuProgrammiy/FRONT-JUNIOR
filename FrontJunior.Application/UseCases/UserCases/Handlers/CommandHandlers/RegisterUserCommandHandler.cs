using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.AuthServices;
using FrontJunior.Application.Services.PasswordServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.Enums;
using FrontJunior.Domain.Entities.Models.OtherModels;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Models.SecondaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IAuthService _authService;
        private readonly IPasswordService _passwordService;

        public RegisterUserCommandHandler(IApplicationDbContext applicationDbContext, IAuthService authService, IPasswordService passwordService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
            _passwordService = passwordService;
        }

        public async Task<ResponseModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

                if (user != null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "Username already taken!"
                    };
                }

                Verification verification = await _applicationDbContext.Verifications.FirstOrDefaultAsync(v => v.Email == request.Email);

                if (verification == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Email not found to verify!"
                    };
                }

                if (verification.SentPassword != request.SentPassword)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "Sent code is incorrect!"
                    };
                }

                PasswordModel passwordModel = _passwordService.HashPassword(request.Password);

                user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = passwordModel.PasswordHash,
                    PassworSalt = passwordModel.PassworSalt,
                    Role = Roles.SimpleUser
                };

                await _applicationDbContext.Users.AddAsync(user);
                _applicationDbContext.Verifications.Remove(verification);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                string token = _authService.GenerateToken(user);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = token
                };
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
