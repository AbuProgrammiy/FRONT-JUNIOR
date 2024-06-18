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
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CreateUserCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rex = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email) != null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "Email is already taken!"
                    };
                }

                if(await _applicationDbContext.Users.FirstOrDefaultAsync(u=>u.Username == request.Username) != null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "Username is already taken!"
                    };
                }

                User user = request.Adapt<User>();

                user.PassworSalt= RandomNumberGenerator.GetBytes(64);
                user.PasswordHash = Convert.ToHexString(
                    Rfc2898DeriveBytes.Pbkdf2(
                        Encoding.UTF8.GetBytes(request.Password),
                        user.PassworSalt,
                        350000,
                        HashAlgorithmName.SHA512,
                        64));

                user.CreatedDate=DateTime.UtcNow;
                user.Role = user.Role == null ? "SimpleUser":user.Role;
                user.SecurityKey=Guid.NewGuid();
                user.IsDeleted = false;

                await _applicationDbContext.Users.AddAsync(user);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Message = "Successfuly created!"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = $"Something went wrong!: {ex.Message}"
                };
            }
        }
    }
}
