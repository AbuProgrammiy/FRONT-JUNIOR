using FrontJunior.Domain.MainModels;
using MediatR;

namespace FrontJunior.Application.UseCases.DataStorageCases.Queries
{
    public class GetDataStorageByIdQuery:IRequest<DataStorage>
    {
        public Guid Id { get; set; }
    }
}
