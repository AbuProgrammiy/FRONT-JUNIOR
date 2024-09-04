using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.MainModels;
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
                List<ActiveTable> tablesByUserId = await _applicationDbContext.ActiveTables.Where(t => t.User.Id == request.UserId).ToListAsync();

                Dictionary<string,IEnumerable<string>> tables= new Dictionary<string,IEnumerable<string>>();

                foreach(ActiveTable tableByUserId in tablesByUserId)
                {
                    List<string> columns = new List<string>();

                    ActiveDataStorage dataStorage = await _applicationDbContext.ActiveDataStorage.FirstOrDefaultAsync(d => d.Table == tableByUserId && d.IsData == false);

                    PropertyInfo[] properties = dataStorage.GetType().GetProperties();

                    foreach(PropertyInfo property in properties)
                    {
                        columns.Add(property.GetValue(dataStorage)?.ToString());
                    }
                    
                    tables.Add(tableByUserId.Name, columns);
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
