using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Commands;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
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

                List<DataStorage> dataStorages = await _applicationDbContext.DataStorage.Where(d => d.Table == table && d.IsData == true).ToListAsync();

                for (int i = 0;i<dataStorages.Count;i++)
                {
                    _applicationDbContext.DataStorage.Remove(dataStorages[i]);
                }

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
