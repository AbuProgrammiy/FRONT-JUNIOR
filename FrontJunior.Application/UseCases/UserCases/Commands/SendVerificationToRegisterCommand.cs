using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class SendVerificationToRegisterCommand:IRequest<ResponseModel>
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
