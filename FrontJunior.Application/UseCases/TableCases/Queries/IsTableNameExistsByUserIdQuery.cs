using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Queries
{
    public class IsTableNameExistsByUserIdQuery:IRequest<ResponseModel>
    {
        public Guid UserId { get; set; }
        public string TableName { get; set; }
    }
}
