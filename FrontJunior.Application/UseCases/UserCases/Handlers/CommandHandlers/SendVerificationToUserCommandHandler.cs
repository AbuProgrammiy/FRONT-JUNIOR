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
    public class SendVerificationToUserCommandHandler : IRequestHandler<SendVerificationToUserCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ISendEmailService _sendEmailService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SendVerificationToUserCommandHandler(IApplicationDbContext applicationDbContext, ISendEmailService sendEmailService, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _sendEmailService = sendEmailService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseModel> Handle(SendVerificationToUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.IsPasswordForgotten == false || request.IsPasswordForgotten == null)
                {
                    if(request.Username == null)
                    {
                        return new ResponseModel
                        {
                            IsSuccess = false,
                            StatusCode = 400,
                            Response = "Username required!"
                        };
                    }

                    User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

                    if (user != null)
                    {
                        return new ResponseModel
                        {
                            IsSuccess = false,
                            StatusCode = 400,
                            Response = "Username already taken!"
                        };
                    }

                    user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

                    if (user != null)
                    {
                        return new ResponseModel
                        {
                            IsSuccess = false,
                            StatusCode = 400,
                            Response = "Email already taken!"
                        };
                    }
                }
                else
                {
                    User user =await _applicationDbContext.Users.FirstOrDefaultAsync(u=>u.Email == request.Email);

                    if (user == null)
                    {
                        return new ResponseModel
                        {
                            IsSuccess = false,
                            StatusCode = 400,
                            Response = "Email not registered yet!"
                        };
                    }
                }

                Verification verification = await _applicationDbContext.Verifications.FirstOrDefaultAsync(v => v.Email == request.Email);

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
                    To = request.Email,
                    Subject = "Email verification!",
                    Body = HTMLbody,
                    IsBodyHTML = true
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
                    Response = $"Something went wrong: {ex.Message}"
                };
            }
        }
    }
}
