using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class VerifyUserCommand:IRequest<ResponseModel>
    {
        public string Email { get; set; }
        public string SentPassword { get; set; }
    }
}
