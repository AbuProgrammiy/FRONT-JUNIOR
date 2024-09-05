using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Queries;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.MainModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.QueryHandlers
{
    public class GetDataStorageByIdQueryHandler : IRequestHandler<GetDataStorageByIdQuery, ActiveDataStorage>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetDataStorageByIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ActiveDataStorage> Handle(GetDataStorageByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.ActiveDataStorage.FirstOrDefaultAsync(d => d.Id == request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
