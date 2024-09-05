using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Queries
{
    public class GetTableByIdQuery:IRequest<ActiveTable>
    {
        public Guid Id { get; set; }
    }
}
