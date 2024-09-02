using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Commands
{
    public class UpdateUserEmailCommand:IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string SentPassword { get; set; }
    }
}
