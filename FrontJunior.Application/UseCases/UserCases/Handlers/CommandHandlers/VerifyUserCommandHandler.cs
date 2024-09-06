using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class VerifyUserCommandHandler : IRequestHandler<VerifyUserCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VerifyUserCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Verification verification =await _applicationDbContext.Verifications.FirstOrDefaultAsync(v=>v.Email==request.Email);

                if (verification == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Email not found!"
                    };
                }

                if (verification.SentPassword != request.SentPassword)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "Password is incorrect!"
                    };
                }

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = "User is successfully verified!"
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
