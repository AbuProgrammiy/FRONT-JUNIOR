using FrontJunior.Domain.Entities;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Queries
{
    public class GetTablesByUserIdQuery:IRequest<IEnumerable<Table>>
    {
        public Guid UserId { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
