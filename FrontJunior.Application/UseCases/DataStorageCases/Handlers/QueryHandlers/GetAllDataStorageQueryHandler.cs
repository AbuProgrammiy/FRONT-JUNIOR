using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Queries;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.MainModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.QueryHandlers
{
    public class GetAllDataStorageQueryHandler : IRequestHandler<GetAllDataStorageQuery, IEnumerable<ActiveDataStorage>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllDataStorageQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<ActiveDataStorage>> Handle(GetAllDataStorageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.ActiveDataStorage.Skip((request.Page - 1) * request.Count)
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
