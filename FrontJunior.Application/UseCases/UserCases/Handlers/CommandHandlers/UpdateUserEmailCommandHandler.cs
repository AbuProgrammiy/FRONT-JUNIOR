using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class UpdateUserEmailCommandHandler : IRequestHandler<UpdateUserEmailCommand, ResponseModel>
    {
        public Task<ResponseModel> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
