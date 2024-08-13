using FrontJunior.Domain.Entities;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Queries
{
    public class GetTableNamesByUserIdQuery:IRequest<IEnumerable<string>>
    {
        public Guid UserId { get; set; }
    }
}
