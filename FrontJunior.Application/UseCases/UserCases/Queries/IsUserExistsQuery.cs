using MediatR;

namespace FrontJunior.Application.UseCases.UserCases.Queries
{
    public class IsUserExistsQuery:IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
