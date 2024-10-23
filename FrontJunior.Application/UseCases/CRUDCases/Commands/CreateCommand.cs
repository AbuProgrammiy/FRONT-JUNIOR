using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.CRUDCases.Commands
{
    public class CreateCommand:IRequest<ResponseModel>
    {
        public string SecurityKey { get; set; } 
        public string TableName { get; set; }
        public object Body { get; set; }
    }
}
