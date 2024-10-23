using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class SendVerificationToUserCommand:IRequest<ResponseModel>
    {
        public string? Username { get; set; }
        public string Email { get; set; }
        public bool? IsPasswordForgotten { get; set; }
    }
}
