using FrontJunior.Domain.Entities.DTOs;
using MediatR;

namespace FrontJunior.Application.UseCases.DataStorageCases.Commands
{
    public class DeleteDataStorageCommand : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
