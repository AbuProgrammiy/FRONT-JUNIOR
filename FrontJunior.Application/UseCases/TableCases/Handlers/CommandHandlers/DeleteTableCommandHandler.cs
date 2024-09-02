using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Commands;
using FrontJunior.Domain.Entities.Views;
using FrontJunior.Domain.MainModels;
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
                Table table = await _applicationDbContext.Tables.Where(t=>t.IsDeleted==false).FirstOrDefaultAsync(t => t.Id == request.Id);

                if(table == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Table not found!"
                    };
                }

                IEnumerable<DataStorage> dataStorages=await _applicationDbContext.DataStorage.Where(d=>d.Table==table).ToListAsync();

                await _applicationDbContext.DeletedDataStorage.AddRangeAsync(dataStorages);
                _applicationDbContext.DataStorage.RemoveRange(dataStorages);

                table.IsDeleted=true;
                table.DeletedDate=DateTime.UtcNow;

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
