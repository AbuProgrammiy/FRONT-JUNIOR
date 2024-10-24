using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Queries
{
    public class GetAllTableQuery:IRequest<ResponseModel>
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
