using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class LogUserInCommand:IRequest<ResponseModel>
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}
