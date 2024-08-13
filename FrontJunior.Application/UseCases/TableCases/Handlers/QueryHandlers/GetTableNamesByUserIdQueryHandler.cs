using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.QueryHandlers
{
    public class GetTableNamesByUserIdQueryHandler : IRequestHandler<GetTableNamesByUserIdQuery, IEnumerable<string>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetTableNamesByUserIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<string>> Handle(GetTableNamesByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> tableNames= new List<string>();

                List<Table> tables = await _applicationDbContext.Tables.Where(t => t.User.Id == request.UserId).ToListAsync();

                for (int i = 0; i < tables.Count; i++)
                {
                    tableNames.Add(tables[i].Name);
                }

                return tableNames;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
