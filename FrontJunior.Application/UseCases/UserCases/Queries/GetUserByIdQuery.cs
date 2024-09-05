using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Queries
{
    public class GetUserByIdQuery:IRequest<ActiveUser>
    {
        public Guid Id { get; set; }
    }
}
