using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Commands;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Views;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.CommandHandlers
{
    public class DeleteTableByUserIdCommandHandler : IRequestHandler<DeleteTableByUserIdCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteTableByUserIdCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(DeleteTableByUserIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ActiveTable table = _applicationDbContext.ActiveTables.FirstOrDefault(t => t.User.Id == request.UserId && t.Name == request.TableName);
                DeletedTable deletedTable = table.Adapt<DeletedTable>();

                if (table == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Table or User not found!"
                    };
                }

                IEnumerable<ActiveDataStorage> dataStorages = await _applicationDbContext.ActiveDataStorage.Where(d => d.Table == table).ToListAsync();

                await _applicationDbContext.DeletedDataStorage.AddRangeAsync(dataStorages.Adapt<IEnumerable<DeletedDataStorage>>());
                _applicationDbContext.ActiveDataStorage.RemoveRange(dataStorages);

                deletedTable.IsDeleted = true;
                deletedTable.DeletedDate= DateTime.UtcNow;

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
