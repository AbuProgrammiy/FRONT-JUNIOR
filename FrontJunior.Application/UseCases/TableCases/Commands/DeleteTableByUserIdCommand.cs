using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Commands
{
    public class DeleteTableByUserIdCommand:IRequest<ResponseModel>
    {
        public Guid UserId { get; set; }
        public string TableName { get; set; }
    }
}
