using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.QueryHandlers
{
    public class GetTablesByUserIdQueryHandler : IRequestHandler<GetTablesByUserIdQuery, IDictionary<string,IEnumerable<string>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetTablesByUserIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IDictionary<string, IEnumerable<string>>> Handle(GetTablesByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Table> tablesByUserId = await _applicationDbContext.Tables.Where(t => t.User.Id == request.UserId).ToListAsync();

                Dictionary<string,IEnumerable<string>> tables= new Dictionary<string,IEnumerable<string>>();

                for (int i = 0; i < tablesByUserId.Count; i++)
                {
                    List<string> columns = new List<string>();

                    DataStorage dataStorage = await _applicationDbContext.DataStorage.FirstOrDefaultAsync(d => d.Table == tablesByUserId[i] && d.IsData == false);

                    PropertyInfo[] properties = dataStorage.GetType().GetProperties();

                    for (int j = 0; j < tablesByUserId[i].ColumnCount; j++)
                    {
                        columns.Add(properties[j].GetValue(dataStorage)?.ToString());
                    }

                    tables.Add(tablesByUserId[i].Name, columns);
                }

                return tables;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
