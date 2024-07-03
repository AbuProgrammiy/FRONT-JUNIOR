using MediatR;

namespace FrontJunior.Application.UseCases.CRUDCases.Queries
{
    public class GetAllQuery:IRequest<object>
    {
        public string SecurityKey { get; set; }
        public string TableName { get; set; }
        public int? Page { get; set; }
        public int? Count { get; set; }
    }
}
