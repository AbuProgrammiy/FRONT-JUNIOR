using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class ResetUserPasswordCommand:IRequest<ResponseModel>
    {
        public string Email { get; set; }
        public string SentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
