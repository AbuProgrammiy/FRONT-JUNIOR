using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Commands;
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
                DataStorage dataStorage =await _applicationDbContext.DataStorage.FirstOrDefaultAsync(d=>d.Id == request.Id);

                if (dataStorage == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "DataStorage is not found!"
                    };
                }

                Table table = await _applicationDbContext.Tables.Where(t => t.IsDeleted == false).FirstOrDefaultAsync(t => t.Id == request.TableId);

                if (table == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Table is not found!"
                    };
                }

                dataStorage=request.Adapt<DataStorage>();
                dataStorage.Table = table;

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
