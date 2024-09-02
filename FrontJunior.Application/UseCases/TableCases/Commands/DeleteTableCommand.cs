using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Commands
{
    public class DeleteTableCommand:IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
