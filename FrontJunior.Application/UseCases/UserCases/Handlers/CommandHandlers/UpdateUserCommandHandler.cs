using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.PasswordServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IPasswordService _passwordService;

        public UpdateUserCommandHandler(IApplicationDbContext applicationDbContext, IPasswordService passwordService)
        {
            _applicationDbContext = applicationDbContext;
            _passwordService = passwordService;
        }

        public async Task<ResponseModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ActiveUser user = await _applicationDbContext.ActiveUsers.FirstOrDefaultAsync(u => u.Id == request.Id);
           
                if (user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "User not found!"
                    };
                }

                if (request.Password != null)
                {
                    bool response= _passwordService.CheckPassword(request.Password, new PasswordModel
                    {
                        PasswordHash = user.PasswordHash,
                        PassworSalt = user.PassworSalt
                    });

                    if(response == false)
                    {
                        return new ResponseModel
                        {
                            IsSuccess = false,
                            StatusCode = 400,
                            Response = "Password is incorrect!"
                        };
                    }
                }

                user.Email=request.FirstName!=null ? request.Email :user.FirstName;
                user.Email=request.LastName!=null ? request.Email :user.LastName;
                user.Email=request.Username!=null ? request.Email :user.Username;
                user.Email=request.Email!=null ? request.Email :user.Email;
                user.Email=request.Role!=null ? request.Email :user.Role;
                user.Email=request.SecurityKey!=null ? request.Email :user.SecurityKey;

                PasswordModel passwordModel = _passwordService.HashPassword(request.Password);

                user.PassworSalt = passwordModel.PassworSalt;
                user.PasswordHash = passwordModel.PasswordHash;

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = "User updated successfully!"
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
