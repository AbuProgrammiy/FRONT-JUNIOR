using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.QueryHandlers
{
    public class GetAllTablesQueryHandler : IRequestHandler<GetAllTableQuery, IEnumerable<ActiveTable>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllTablesQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<ActiveTable>> Handle(GetAllTableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.ActiveTables.OrderBy(t => t.CreatedDate)
                                                               .Skip((request.Page-1)*request.Count)
                                                               .Take(request.Count)
                                                               .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
