using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
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
                Table table = _applicationDbContext.Tables.FirstOrDefault(t => t.Name == request.TableName && t.User.Id == request.UserId);

                if (table == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Table not found or UserId is incorret!"
                    };
                }

                IEnumerable<DataStorage> dataStorages = await _applicationDbContext.DataStorage.Where(d => d.Table == table).ToListAsync();

                _applicationDbContext.DataStorage.RemoveRange(dataStorages);
                _applicationDbContext.Tables.Remove(table);

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
