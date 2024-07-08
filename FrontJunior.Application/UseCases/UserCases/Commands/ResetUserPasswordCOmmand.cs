using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class ResetUserPasswordCOmmand:IRequest<object>
    {
        public string Email { get; set; }
        public string SentPassword { get; set; }
    }
}
