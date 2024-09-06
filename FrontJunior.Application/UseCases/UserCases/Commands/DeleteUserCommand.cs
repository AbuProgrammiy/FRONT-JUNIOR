using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class DeleteUserCommand:IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
