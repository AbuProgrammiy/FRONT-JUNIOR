using FrontJunior.Domain.Entities.DTOs;
using FrontJunior.Domain.Entities.Models;

namespace FrontJunior.Application.Services.EmailServices
{
    public interface ISendEmailService
    {
        public Task<ResponseModel> SendEmailAsync(EmailDTO emailDTO);
    }
}
