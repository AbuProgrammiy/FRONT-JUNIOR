using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class UpdateUserSecurityKeyCommand:IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
        public string NewSecurityKey { get; set; }
    }
}
