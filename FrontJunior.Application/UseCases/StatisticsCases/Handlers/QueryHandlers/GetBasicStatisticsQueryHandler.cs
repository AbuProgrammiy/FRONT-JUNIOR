using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.StatisticsCases.Queries;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.StatisticsCases.Handlers.QueryHandlers
{
    public class GetBasicStatisticsQueryHandler : IRequestHandler<GetBasicStatisticsQuery, BasicStatistics>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetBasicStatisticsQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<BasicStatistics> Handle(GetBasicStatisticsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                BasicStatistics basicStatistics = new BasicStatistics
                {
                    UsersCount = (await _applicationDbContext.Users.ToListAsync()).Count,
                    TablesCount = (await _applicationDbContext.Tables.ToListAsync()).Count,
                };

                return basicStatistics;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
