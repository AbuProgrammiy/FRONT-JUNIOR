using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.UserCases.Queries;
using FrontJunior.Domain.Entities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.QueryHandlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<ActiveUser>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllUsersQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<ActiveUser>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.ActiveUsers.OrderBy(u=>u.CreatedDate)
                                                              .Skip((request.Page-1)*request.Count)
                                                              .Take(request.Count)
                                                              .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
