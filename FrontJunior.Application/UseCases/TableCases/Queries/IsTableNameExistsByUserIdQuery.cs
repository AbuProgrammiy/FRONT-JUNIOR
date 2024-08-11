using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Queries
{
    public class IsTableNameExistsByUserIdQuery:IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string TableName { get; set; }
    }
}
