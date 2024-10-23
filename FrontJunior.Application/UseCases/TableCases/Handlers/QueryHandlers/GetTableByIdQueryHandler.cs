using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.QueryHandlers
{
    public class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, Table>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetTableByIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Table> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.Tables.FirstOrDefaultAsync(t => t.Id == request.Id);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
