using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Queries;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.QueryHandlers
{
    public class GetColumnsByTableIdQueryHandler : IRequestHandler<GetColumnsByTableIdQuery, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetColumnsByTableIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(GetColumnsByTableIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Table table = await _applicationDbContext.Tables.FirstOrDefaultAsync(t => t.Id == request.TableId);

                if (table == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Table not found!"
                    };
                }

                List<string> columns = new List<string>();

                DataStorage dataStorage = await _applicationDbContext.DataStorage.FirstOrDefaultAsync(d => d.Table.Id == request.TableId && d.IsData == false);

                PropertyInfo[] properties = dataStorage.GetType().GetProperties();

                for (int i = 0; i < table.ColumnCount; i++)
                {
                    columns.Add(properties[i].GetValue(dataStorage)?.ToString());
                }

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = columns
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
