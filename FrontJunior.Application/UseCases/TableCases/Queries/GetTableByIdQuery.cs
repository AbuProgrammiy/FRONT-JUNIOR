using FrontJunior.Domain.Entities.Models.PrimaryModels;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Queries
{
    public class GetTableByIdQuery:IRequest<Table>
    {
        public Guid Id { get; set; }
    }
}
