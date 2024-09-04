using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.AuthServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class UpdateUserEmailCommandHandler : IRequestHandler<UpdateUserEmailCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;

        public UpdateUserEmailCommandHandler(IApplicationDbContext applicationDbContext, IAuthService authService, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
            _mediator = mediator;
        }

        public async Task<ResponseModel> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ResponseModel response = await _mediator.Send(new VerifyUserCommand
                {
                    Email = request.Email,
                    SentPassword = request.SentPassword,
                });

                if (response.IsSuccess == false)
                {
                    return response;
                }

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

                user.Email = request.Email;

                _applicationDbContext.Verifications.Remove(await _applicationDbContext.Verifications.FirstOrDefaultAsync(v => v.Email == request.Email));
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                TokenModel tokenModel =_authService.GenerateToken(user);

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
