using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Queries
{
    public class GetAllUsersQuery:IRequest<IEnumerable<ActiveUser>>
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
