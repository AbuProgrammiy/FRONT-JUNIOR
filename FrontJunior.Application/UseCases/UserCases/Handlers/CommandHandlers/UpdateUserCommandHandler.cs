using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.PasswordServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
using Mapster;
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
                User user = await _applicationDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == request.Id);
           
                if(user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "User not found!"
                    };
                }

                user = request.Adapt<User>();

                PasswordModel passwordModel = _passwordService.HashPassword(request.Password);

                user.PassworSalt = passwordModel.PassworSalt;
                user.PasswordHash = passwordModel.PasswordHash;

                _applicationDbContext.Users.Update(user);
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
