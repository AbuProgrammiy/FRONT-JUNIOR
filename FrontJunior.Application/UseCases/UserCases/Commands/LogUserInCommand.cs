using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class LogUserInCommand:IRequest<object>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
