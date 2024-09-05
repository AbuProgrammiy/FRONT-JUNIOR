using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.DataStorageCases.Queries
{
    public class GetAllDataStorageQuery:IRequest<IEnumerable<ActiveDataStorage>>
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
