using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.CommandHandlers
{
    public class CreateDataStorageCommandHandler : IRequestHandler<CreateDataStorageCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CreateDataStorageCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(CreateDataStorageCommand request, CancellationToken cancellationToken)
        {
            try
            {
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

                DataStorage dataStorage = request.Adapt<DataStorage>();
                dataStorage.Table = table;

                await _applicationDbContext.DataStorage.AddAsync(dataStorage);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Response = "DataStorage successfully created!"
                };
            }
            catch(Exception ex)
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
