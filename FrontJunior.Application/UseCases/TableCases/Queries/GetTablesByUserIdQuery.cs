using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Queries
{
    public class GetTablesByUserIdQuery:IRequest<ResponseModel>
    {
        public Guid UserId { get; set; }
    }
}
