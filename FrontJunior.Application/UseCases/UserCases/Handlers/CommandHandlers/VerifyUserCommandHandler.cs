using FrontJunior.Application.Abstractions;
using FrontJunior.Application.Services.EmailServices;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.DTOs;
using FrontJunior.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using FrontJunior.Domain.Entities.Models;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class VerifyUserCommandHandler : IRequestHandler<VerifyUserCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ISendEmailService _sendEmailService;
        private readonly IConfiguration _configuration;

        public VerifyUserCommandHandler(IApplicationDbContext applicationDbContext, ISendEmailService sendEmailService, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _sendEmailService = sendEmailService;
            _configuration = configuration;
        }

        public async Task<ResponseModel> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
        {
            if (await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email) != null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "Email is already taken!"
                };
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
    }
}
