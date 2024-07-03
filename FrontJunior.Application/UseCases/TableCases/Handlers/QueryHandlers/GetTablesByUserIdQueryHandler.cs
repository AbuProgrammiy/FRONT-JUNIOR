using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.QueryHandlers
{
    public class GetTablesByUserIdQueryHandler : IRequestHandler<GetTablesByUserIdQuery, IEnumerable<Table>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetTablesByUserIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Table>> Handle(GetTablesByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.Tables.Where(t=>t.User.Id==request.UserId)
                                                         .OrderBy(t=>t.CreatedDate)
                                                         .Skip((request.Page-1)*request.Count)
                                                         .Take(request.Count)
                                                         .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
