using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.PasswordServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Views;
using FrontJunior.Domain.MainModels;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IPasswordService _passwordService;

        public CreateUserCommandHandler(IApplicationDbContext applicationDbContext, IPasswordService passwordService)
        {
            _applicationDbContext = applicationDbContext;
            _passwordService = passwordService;
        }

        public async Task<ResponseModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _applicationDbContext.ActiveUsers.FirstOrDefaultAsync(u => u.Email == request.Email) != null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "Email is already taken!"
                    };
                }

                if (request.Username != null && await _applicationDbContext.ActiveUsers.FirstOrDefaultAsync(u => u.Username == request.Username) != null) 
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "Username is already taken!"
                    };
                }

                ActiveUser activeUser = request.Adapt<ActiveUser>();

                PasswordModel passwordModel = _passwordService.HashPassword(request.Password);

                activeUser.PassworSalt= passwordModel.PassworSalt;
                activeUser.PasswordHash = passwordModel.PasswordHash;

                activeUser.CreatedDate=DateTime.UtcNow;
                activeUser.Role = activeUser.Role == null ? "SimpleUser": activeUser.Role;
                activeUser.SecurityKey=Guid.NewGuid().ToString();

                await _applicationDbContext.ActiveUsers.AddAsync(activeUser);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Response = "Successfuly created!"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Response = $"Something went wrong!: {ex.Message}"
                };
            }
        }
    }
}
