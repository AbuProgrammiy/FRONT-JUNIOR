using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Queries;
using FrontJunior.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.QueryHandlers
{
    public class GetAllDataStorageQueryHandler : IRequestHandler<GetAllDataStorageQuery, IEnumerable<DataStorage>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllDataStorageQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<DataStorage>> Handle(GetAllDataStorageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.DataStorage.Skip(request.Page)
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
