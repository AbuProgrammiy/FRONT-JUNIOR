using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Queries
{
    public class GetAllUsersQuery:IRequest<ResponseModel>
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
