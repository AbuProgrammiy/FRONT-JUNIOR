using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Queries
{
    public class IsUserExistsQuery:IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
