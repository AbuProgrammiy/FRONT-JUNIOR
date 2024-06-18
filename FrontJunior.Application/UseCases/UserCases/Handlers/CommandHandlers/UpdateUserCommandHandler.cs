using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.DTOs;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UpdateUserCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
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
                        Message = "User not found!"
                    };
                }

                user = request.Adapt<User>();

                user.PassworSalt = RandomNumberGenerator.GetBytes(64);
                user.PasswordHash = Convert.ToHexString(
                    Rfc2898DeriveBytes.Pbkdf2(
                        Encoding.UTF8.GetBytes(request.Password),
                        user.PassworSalt,
                        350000,
                        HashAlgorithmName.SHA512,
                        64));

                _applicationDbContext.Users.Update(user);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "User updated successfully!"
                };
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
