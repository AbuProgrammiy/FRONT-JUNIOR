using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.QueryHandlers
{
    public class GetAllTablesQueryHandler : IRequestHandler<GetAllTableQuery, IEnumerable<Table>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllTablesQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Table>> Handle(GetAllTableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.Tables.Where(t => t.IsDeleted == false)
                                                        .Skip(request.Page)
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
