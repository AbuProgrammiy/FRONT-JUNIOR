using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Commands;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Views;
using FrontJunior.Domain.MainModels;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.CommandHandlers
{
    public class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteTableCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ActiveTable table = await _applicationDbContext.ActiveTables.FirstOrDefaultAsync(t => t.Id == request.Id);
                DeletedTable deletedTable = table.Adapt<DeletedTable>();

                if(table == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Table not found!"
                    };
                }

                IEnumerable<ActiveDataStorage> dataStorages=await _applicationDbContext.ActiveDataStorage.Where(d=>d.Table==table).ToListAsync();

                await _applicationDbContext.DeletedDataStorage.AddRangeAsync(dataStorages.Adapt<IEnumerable<DeletedDataStorage>>());
                _applicationDbContext.ActiveDataStorage.RemoveRange(dataStorages);

                deletedTable.IsDeleted=true;
                deletedTable.DeletedDate=DateTime.UtcNow;

                await _applicationDbContext.DeletedTables.AddAsync(deletedTable);
                _applicationDbContext.ActiveTables.Remove(table);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = "Table successfully deleted!"
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
