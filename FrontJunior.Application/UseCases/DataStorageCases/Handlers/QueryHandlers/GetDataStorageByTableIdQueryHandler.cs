using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Queries;
using FrontJunior.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.QueryHandlers
{
    public class GetDataStorageByTableIdQueryHandler : IRequestHandler<GetDataStorageByTableIdQuery, IEnumerable<DataStorage>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetDataStorageByTableIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<DataStorage>> Handle(GetDataStorageByTableIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.DataStorage.Where(d => d.Table.Id == request.TableId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
