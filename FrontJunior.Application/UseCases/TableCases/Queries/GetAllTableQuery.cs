using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Queries
{
    public class GetAllTableQuery:IRequest<IEnumerable<ActiveTable>>
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
