using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class SendVerificationToUserCommand:IRequest<ResponseModel>
    {
        public string Email { get; set; }
        public bool? IsPasswordForgotten { get; set; }
    }
}
