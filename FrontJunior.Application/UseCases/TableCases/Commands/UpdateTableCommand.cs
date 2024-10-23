using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
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
