using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Queries
{
    public class GetTableByIdQuery:IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
