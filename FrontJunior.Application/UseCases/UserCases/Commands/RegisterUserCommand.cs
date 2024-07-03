using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class RegisterUserCommand:IRequest<object>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SentPassword { get; set; }
    }
}
