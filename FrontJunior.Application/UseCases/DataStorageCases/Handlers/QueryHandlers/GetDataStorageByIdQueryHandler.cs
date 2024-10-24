using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.DataStorageCases.Queries;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.DataStorageCases.Handlers.QueryHandlers
{
    public class GetDataStorageByIdQueryHandler : IRequestHandler<GetDataStorageByIdQuery, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetDataStorageByIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(GetDataStorageByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                DataStorage dataStorage = await _applicationDbContext.DataStorage.FirstOrDefaultAsync(d => d.Id == request.Id);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = dataStorage
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
