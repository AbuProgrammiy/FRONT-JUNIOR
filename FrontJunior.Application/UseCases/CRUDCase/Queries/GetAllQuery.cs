using MediatR;

namespace FrontJunior.Application.UseCases.CRUDCase.Queries
{
    public class GetAllQuery:IRequest<object>
    {
        public Guid SecurityKey { get; set; }
        public string TableName { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
