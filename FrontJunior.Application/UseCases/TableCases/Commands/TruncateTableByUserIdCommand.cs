using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Commands
{
    public class TruncateTableByUserIdCommand:IRequest<ResponseModel>
    {
        public Guid UserId { get; set; }
        public string TableName { get; set; }
    }
}
