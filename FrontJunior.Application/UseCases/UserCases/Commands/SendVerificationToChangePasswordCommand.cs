using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class SendVerificationToChangePasswordCommand:IRequest<ResponseModel>
    {
        public string Email { get; set; }
    }
}
