using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Queries;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.QueryHandlers
{
    public class GetAllDataStorageColumnsQueryHandler:IRequestHandler<GetAllDataStorageColumnsQuery,ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllDataStorageColumnsQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(GetAllDataStorageColumnsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<DataStorage> dataStorages = await _applicationDbContext.DataStorage.Where(d => d.IsData == false)
                                                                                               .Skip((request.Page - 1) * request.Count)
                                                                                               .Take(request.Count)
                                                                                               .ToListAsync();

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = dataStorages
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Response = $"Something went wrong: {ex.Message}"
                };
            }
        }
    }
}
