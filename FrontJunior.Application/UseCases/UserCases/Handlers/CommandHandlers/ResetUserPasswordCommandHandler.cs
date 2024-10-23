using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.AuthServices;
using FrontJunior.Application.Services.PasswordServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.Models.OtherModels;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Models.SecondaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IAuthService _authService;
        private readonly IPasswordService _passwordService;

        public ResetUserPasswordCommandHandler(IApplicationDbContext applicationDbContext, IAuthService authService, IPasswordService passwordService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
            _passwordService = passwordService;
        }

        public async Task<ResponseModel> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
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
                        Response = "Sent code is incorret!"
                    };
                }

                PasswordModel passwordModel = _passwordService.HashPassword(request.NewPassword);

                User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

                user.PasswordHash = passwordModel.PasswordHash;
                user.PassworSalt = passwordModel.PassworSalt;

                await _applicationDbContext.SaveChangesAsync(cancellationToken);
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
