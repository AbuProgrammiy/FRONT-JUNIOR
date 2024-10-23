using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Queries
{
    public class GetUserByIdQuery:IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
