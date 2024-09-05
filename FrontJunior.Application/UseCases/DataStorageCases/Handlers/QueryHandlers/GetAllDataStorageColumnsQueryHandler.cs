using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Queries;
using FrontJunior.Domain.Entities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.QueryHandlers
{
    public class GetAllDataStorageColumnsQueryHandler:IRequestHandler<GetAllDataStorageColumnsQuery,IEnumerable<ActiveDataStorage>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllDataStorageColumnsQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<ActiveDataStorage>> Handle(GetAllDataStorageColumnsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.ActiveDataStorage.Where(d=>d.IsData==false)
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
