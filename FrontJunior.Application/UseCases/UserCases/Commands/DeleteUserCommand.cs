using FrontJunior.Domain.Entities.DTOs;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class DeleteUserCommand:IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
