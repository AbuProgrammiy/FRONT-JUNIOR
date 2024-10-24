using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.CRUDCases.Queries
{
    public class GetAllQuery:IRequest<ResponseModel>
    {
        public string Username { get; set; }
        public string TableName { get; set; }
        public int? Page { get; set; }
        public int? Count { get; set; }
    }
}
