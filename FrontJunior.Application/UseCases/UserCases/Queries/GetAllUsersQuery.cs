using FrontJunior.Domain.Entities;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Queries
{
    public class GetAllUsersQuery:IRequest<IEnumerable<User>>
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
