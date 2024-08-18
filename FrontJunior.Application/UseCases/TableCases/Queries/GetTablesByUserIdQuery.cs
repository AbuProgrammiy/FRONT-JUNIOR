using FrontJunior.Domain.Entities;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Queries
{
    public class GetTablesByUserIdQuery:IRequest<IDictionary<string,IEnumerable<string>>>
    {
        public Guid UserId { get; set; }
    }
}
