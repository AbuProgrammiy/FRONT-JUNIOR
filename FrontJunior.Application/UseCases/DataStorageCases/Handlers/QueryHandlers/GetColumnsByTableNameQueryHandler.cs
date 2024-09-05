using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Queries;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.MainModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.QueryHandlers
{
    public class GetColumnsByTableNameQueryHandler : IRequestHandler<GetColumnsByTableNameQuery, IEnumerable<string>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetColumnsByTableNameQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<string>> Handle(GetColumnsByTableNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> columns = new List<string>();

                ActiveTable table = await _applicationDbContext.ActiveTables.FirstOrDefaultAsync(t => t.User.Id == request.UserId && t.Name == request.TableName);

                ActiveDataStorage dataStorage = await _applicationDbContext.ActiveDataStorage.FirstOrDefaultAsync(d => d.Table.User.Id == request.UserId&&d.Table==table && d.IsData == false);

                PropertyInfo[] properties = dataStorage.GetType().GetProperties();

                for (int i = 0; i < table.ColumnCount; i++)
                {
                    columns.Add(properties[i].GetValue(dataStorage)?.ToString());
                }

                return columns;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
