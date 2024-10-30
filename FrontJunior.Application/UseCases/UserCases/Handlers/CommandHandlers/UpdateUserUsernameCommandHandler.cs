using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.AuthServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class UpdateUserUsernameCommandHandler : IRequestHandler<UpdateUserUsernameCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IAuthService _authService;

        public UpdateUserUsernameCommandHandler(IApplicationDbContext applicationDbContext, IAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }

        public async Task<ResponseModel> Handle(UpdateUserUsernameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Username == request.NewUsername);

                if (user != null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "Username already taken"
                    };
                }

                user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.Id);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "User not found to update username!"
                    };
                }

                user.Username = request.NewUsername;

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
