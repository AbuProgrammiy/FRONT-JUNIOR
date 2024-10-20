using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.StatisticsCases.Queries
{
    public class GetBasicStatisticsQuery:IRequest<BasicStatistics>
    {
    }
}
