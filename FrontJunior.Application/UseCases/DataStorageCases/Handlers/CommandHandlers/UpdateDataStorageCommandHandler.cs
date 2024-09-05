using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Commands;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Views;
using FrontJunior.Domain.MainModels;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.CommandHandlers
{
    public class UpdateDataStorageCommandHandler : IRequestHandler<UpdateDataStorageCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UpdateDataStorageCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(UpdateDataStorageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ActiveDataStorage dataStorage =await _applicationDbContext.ActiveDataStorage.AsNoTracking().FirstOrDefaultAsync(d=>d.Id == request.Id);

                if (dataStorage == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "DataStorage is not found!"
                    };
                }

                ActiveTable table = await _applicationDbContext.ActiveTables.FirstOrDefaultAsync(t => t.Id == request.TableId);

                if (table == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Table is not found!"
                    };
                }

                dataStorage=request.Adapt<ActiveDataStorage>();
                dataStorage.Table = table;

                _applicationDbContext.ActiveDataStorage.Update(dataStorage);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = "DataSotarage successfully updated!"
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
