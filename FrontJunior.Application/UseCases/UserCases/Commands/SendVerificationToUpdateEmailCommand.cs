using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class SendVerificationToUpdateEmailCommand:IRequest<ResponseModel>
    {
        public string NewEmail { get; set; }
    }
}
