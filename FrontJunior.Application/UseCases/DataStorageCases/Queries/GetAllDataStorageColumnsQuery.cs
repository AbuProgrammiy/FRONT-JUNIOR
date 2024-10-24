using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.DataStorageCases.Queries
{
    public class GetAllDataStorageColumnsQuery:IRequest<ResponseModel>
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
