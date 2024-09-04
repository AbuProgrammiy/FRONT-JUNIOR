using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Commands;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.CommandHandlers
{
    public class TruncateTableByUserIdCommandHanlder : IRequestHandler<TruncateTableByUserIdCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public TruncateTableByUserIdCommandHanlder(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(TruncateTableByUserIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ActiveTable table = _applicationDbContext.ActiveTables.FirstOrDefault(t => t.Name == request.TableName && t.User.Id == request.UserId);

                if (table == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Table not found or UserId is incorret!"
                    };
                }

                IEnumerable<ActiveDataStorage> dataStorages = await _applicationDbContext.ActiveDataStorage.Where(d => d.Table == table && d.IsData == true).ToListAsync();
                _applicationDbContext.ActiveDataStorage.RemoveRange(dataStorages);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = "Table successfuly truncated!"
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
