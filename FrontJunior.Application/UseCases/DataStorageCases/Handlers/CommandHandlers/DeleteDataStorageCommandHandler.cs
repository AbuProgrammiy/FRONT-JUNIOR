using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Commands;
using FrontJunior.Domain.Entities.Views;
using FrontJunior.Domain.MainModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.CommandHandlers
{
    public class DeleteDataStorageCommandHandler : IRequestHandler<DeleteDataStorageCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteDataStorageCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(DeleteDataStorageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                DataStorage dataStorage = await _applicationDbContext.DataStorage.FirstOrDefaultAsync(d => d.Id == request.Id);

                if (dataStorage == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "DataStorage is not found!"
                    };
                }

                await _applicationDbContext.DeletedDataStorage.AddAsync(dataStorage);
                _applicationDbContext.DataStorage.Remove(dataStorage);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = "DataStorage is successfully deleted!"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Response = $"Something went wrong: {ex.Message}"
                };
            }
        }
    }
}
