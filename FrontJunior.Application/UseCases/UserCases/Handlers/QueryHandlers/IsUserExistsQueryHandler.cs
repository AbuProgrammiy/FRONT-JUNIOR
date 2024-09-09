using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.UserCases.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.QueryHandlers
{
    public class IsUserExistsQueryHandler : IRequestHandler<IsUserExistsQuery, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public IsUserExistsQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(IsUserExistsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.Id) != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
