using FrontJunior.Domain.MainModels;
using MediatR;

namespace FrontJunior.Application.UseCases.DataStorageCases.Queries
{
    public class GetAllDataStorageQuery:IRequest<IEnumerable<DataStorage>>
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
