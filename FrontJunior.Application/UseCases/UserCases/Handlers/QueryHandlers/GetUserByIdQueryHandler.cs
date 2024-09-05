using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.UserCases.Queries;
using FrontJunior.Domain.Entities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.QueryHandlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ActiveUser>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetUserByIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ActiveUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.ActiveUsers.FirstOrDefaultAsync(u => u.Id == request.Id);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
