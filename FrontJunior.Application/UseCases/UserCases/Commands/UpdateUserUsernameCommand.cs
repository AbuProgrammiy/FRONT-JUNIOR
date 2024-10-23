using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class UpdateUserUsernameCommand:IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
        public string NewUsername { get; set; }
    }
}
