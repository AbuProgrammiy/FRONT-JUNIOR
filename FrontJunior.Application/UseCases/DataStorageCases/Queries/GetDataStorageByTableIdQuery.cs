using FrontJunior.Domain.Entities;
using MediatR;

namespace FrontJunior.Application.UseCases.DataStorageCases.Queries
{
    public class GetDataStorageByTableIdQuery:IRequest<DataStorage>
    {
        public Guid TableId { get; set; }
    }
}
