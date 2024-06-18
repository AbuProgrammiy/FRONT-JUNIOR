using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Queries;
using FrontJunior.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.QueryHandlers
{
    public class GetAllDataStorageColumnsQueryHandler:IRequestHandler<GetAllDataStorageColumnsQuery,IEnumerable<DataStorage>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllDataStorageColumnsQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<DataStorage>> Handle(GetAllDataStorageColumnsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.DataStorage.Where(d=>d.IsData==false)
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
