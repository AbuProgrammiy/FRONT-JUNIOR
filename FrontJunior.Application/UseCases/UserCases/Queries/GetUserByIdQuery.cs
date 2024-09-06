using FrontJunior.Domain.Entities;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Queries
{
    public class GetUserByIdQuery:IRequest<User>
    {
        public Guid Id { get; set; }
    }
}
