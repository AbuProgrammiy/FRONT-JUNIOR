using FrontJunior.Domain.Entities.DTOs;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Commands
{
    public class CreateTableCommand:IRequest<ResponseModel>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public byte ColumnCount { get; set; }
    }
}
