using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.AuthServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Models.SecondaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class UpdateUserEmailCommandHandler : IRequestHandler<UpdateUserEmailCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IAuthService _authService;

        public UpdateUserEmailCommandHandler(IApplicationDbContext applicationDbContext, IAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }

        public async Task<ResponseModel> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.Id);

                if(user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "User not found to update email!"
                    };
                }

                Verification verification = await _applicationDbContext.Verifications.FirstOrDefaultAsync(v => v.Email == request.NewEmail);

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
                        IsSuccess = true,
                        StatusCode = 400,
                        Response = "Sent code is incorrect!"
                    };
                }

                user.Email = request.NewEmail;

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
