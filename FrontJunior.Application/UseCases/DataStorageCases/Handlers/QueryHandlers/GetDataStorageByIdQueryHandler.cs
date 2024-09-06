using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Queries;
using FrontJunior.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.QueryHandlers
{
    public class GetDataStorageByIdQueryHandler : IRequestHandler<GetDataStorageByIdQuery, DataStorage>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetDataStorageByIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<DataStorage> Handle(GetDataStorageByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.DataStorage.FirstOrDefaultAsync(d => d.Id == request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
