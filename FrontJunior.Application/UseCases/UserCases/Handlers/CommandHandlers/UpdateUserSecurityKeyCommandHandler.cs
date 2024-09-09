using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.AuthServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class UpdateUserSecurityKeyCommandHandler : IRequestHandler<UpdateUserSecurityKeyCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IAuthService _authService;

        public UpdateUserSecurityKeyCommandHandler(IApplicationDbContext applicationDbContext, IAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }

        public async Task<ResponseModel> Handle(UpdateUserSecurityKeyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.Id);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "User not found!"
                    };
                }
                user.SecurityKey = request.NewSecurityKey;

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                TokenModel tokenModel = _authService.GenerateToken(user);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = tokenModel.Token
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Response = "Something went wrong!"
                };
            }
        }
    }
}
