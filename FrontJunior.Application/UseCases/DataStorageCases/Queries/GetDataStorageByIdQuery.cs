using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.DataStorageCases.Queries
{
    public class GetDataStorageByIdQuery:IRequest<ActiveDataStorage>
    {
        public Guid Id { get; set; }
    }
}
