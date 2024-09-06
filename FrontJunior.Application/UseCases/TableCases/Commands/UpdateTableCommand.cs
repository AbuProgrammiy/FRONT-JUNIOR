using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Commands
{
    public class UpdateTableCommand:IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public byte ColumnCount { get; set; }
    }
}
