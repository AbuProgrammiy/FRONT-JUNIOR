using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.EmailServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.DTOs;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Models.SecondaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class SendVerificationToUpdateEmailCommandHandler : IRequestHandler<SendVerificationToUpdateEmailCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ISendEmailService _sendEmailService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SendVerificationToUpdateEmailCommandHandler(IApplicationDbContext applicationDbContext, ISendEmailService sendEmailService, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _sendEmailService = sendEmailService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseModel> Handle(SendVerificationToUpdateEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.NewEmail);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "Email already taken!"
                    };
                }

                Verification verification = await _applicationDbContext.Verifications.FirstOrDefaultAsync(v => v.Email == request.NewEmail);

                if (verification != null)
                {
                    _applicationDbContext.Verifications.Remove(verification);
                }

                Random random = new Random();

                string password = random.Next(100000, 999999).ToString();

                string HTMLbody;

                string verificationMessagePath = $"{_webHostEnvironment.WebRootPath}/HTMLMessages/Verification.html";

                Console.WriteLine($"\n\n{_webHostEnvironment.WebRootPath}\n\n");

                using (StreamReader stream = new StreamReader(verificationMessagePath))
                {
                    HTMLbody = (await stream.ReadToEndAsync()).Replace("verification-code", password);
                }

                ResponseModel response = await _sendEmailService.SendEmailAsync(new EmailDTO
                {
                    To = request.NewEmail,
                    Subject = "Email verification!",
                    Body = HTMLbody,
                    IsBodyHTML = true
                });

                if (response.IsSuccess)
                {
                    await _applicationDbContext.Verifications.AddAsync(new Verification
                    {
                        Email = request.NewEmail,
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
                    Response = $"Something went wrong: {ex.Message}"
                };
            }
        }
    }
}
