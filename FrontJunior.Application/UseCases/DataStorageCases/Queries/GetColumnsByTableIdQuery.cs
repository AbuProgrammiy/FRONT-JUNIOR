using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.DataStorageCases.Queries
{
    public class GetColumnsByTableIdQuery:IRequest<ResponseModel>
    {
        public Guid TableId { get; set; }
    }
}
