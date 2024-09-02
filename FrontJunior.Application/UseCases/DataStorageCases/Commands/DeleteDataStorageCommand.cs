using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.DataStorageCases.Commands
{
    public class DeleteDataStorageCommand : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
