using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class VerifyUserCommand:IRequest<ResponseModel>
    {
        public string Email { get; set; }
    }
}
