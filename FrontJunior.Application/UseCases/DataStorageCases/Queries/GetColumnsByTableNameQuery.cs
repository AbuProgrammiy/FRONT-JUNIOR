using FrontJunior.Domain.Entities;
using MediatR;

namespace FrontJunior.Application.UseCases.DataStorageCases.Queries
{
    public class GetColumnsByTableNameQuery:IRequest<IEnumerable<string>>
    {
        public Guid UserId { get; set; }
        public string TableName { get; set; }
    }
}
