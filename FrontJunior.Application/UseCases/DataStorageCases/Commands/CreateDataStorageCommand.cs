using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.DataStorageCases.Commands
{
    public class CreateDataStorageCommand:IRequest<ResponseModel>
    {
        public bool IsData { get; set; }
        public Guid TableId { get; set; }
        public string Column1 { get; set; }
        public string? Column2 { get; set; }
        public string? Column3 { get; set; }
        public string? Column4 { get; set; }
        public string? Column5 { get; set; }
        public string? Column6 { get; set; }
        public string? Column7 { get; set; }
        public string? Column8 { get; set; }
        public string? Column9 { get; set; }
        public string? Column10 { get; set; }
        public string? Column11 { get; set; }
        public string? Column12 { get; set; }
        public string? Column13 { get; set; }
        public string? Column14 { get; set; }
        public string? Column15 { get; set; }
        public string? Column16 { get; set; }
        public string? Column17 { get; set; }
        public string? Column18 { get; set; }
        public string? Column19 { get; set; }
        public string? Column20 { get; set; }
    }
}
