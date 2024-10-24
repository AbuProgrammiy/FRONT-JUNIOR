using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.StatisticsCases.Queries;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.StatisticsCases.Handlers.QueryHandlers
{
    public class GetBasicStatisticsQueryHandler : IRequestHandler<GetBasicStatisticsQuery, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetBasicStatisticsQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(GetBasicStatisticsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                BasicStatistics basicStatistics = new BasicStatistics
                {
                    UsersCount = (await _applicationDbContext.Users.ToListAsync()).Count,
                    TablesCount = (await _applicationDbContext.Tables.ToListAsync()).Count,
                };

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = basicStatistics
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
