using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.DataStorageCases.Queries
{
    public class GetDataStorageByIdQuery:IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
