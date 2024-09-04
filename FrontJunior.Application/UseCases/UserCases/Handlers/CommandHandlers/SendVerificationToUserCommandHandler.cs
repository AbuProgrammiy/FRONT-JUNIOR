using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.EmailServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.DTOs;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Views;
using FrontJunior.Domain.MainModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class SendVerificationToUserCommandHandler : IRequestHandler<SendVerificationToUserCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ISendEmailService _sendEmailService;
        private readonly IConfiguration _configuration;

        public SendVerificationToUserCommandHandler(IApplicationDbContext applicationDbContext, ISendEmailService sendEmailService, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _sendEmailService = sendEmailService;
            _configuration = configuration;
        }

        public async Task<ResponseModel> Handle(SendVerificationToUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ActiveUser user = await _applicationDbContext.ActiveUsers.FirstOrDefaultAsync(u => u.Email == request.Email);

                if (request.IsPasswordForgotten == null && user != null || request.IsPasswordForgotten == false && user != null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "Email is already taken!"
                    };
                }
                else if(request.IsPasswordForgotten==true&&user ==null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Email not resgistered yet!!"
                    };
                }
            

                Verification verification=await _applicationDbContext.Verifications.FirstOrDefaultAsync(v=>v.Email == request.Email);

                if (verification!=null)
                {
                    _applicationDbContext.Verifications.Remove(verification);
                }

                Random random = new Random();

                string password = random.Next(100000, 999999).ToString();

                string HTMLbody;

                using (StreamReader stream=new StreamReader(_configuration["HTMLmessagePath"]))
                {
                    HTMLbody = (await stream.ReadToEndAsync()).Replace("verification-code", password);
                }

                ResponseModel response = await _sendEmailService.SendEmailAsync(new EmailDTO
                {
                    To = request.Email,
                    Subject = "Email verification!",
                    Body = HTMLbody,
                    IsBodyHTML=true
                });

                if (response.IsSuccess)
                {
                    await _applicationDbContext.Verifications.AddAsync(new Verification
                    {
                        Email = request.Email,
                        SentPassword = password
                    });

                    await _applicationDbContext.SaveChangesAsync(cancellationToken);
                }

                return response;
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Response = "Someting went wrong!"
                };
            }
        }
    }
}
