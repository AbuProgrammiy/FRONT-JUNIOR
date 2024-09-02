using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.UserCases.Queries;
using FrontJunior.Domain.MainModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.QueryHandlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllUsersQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.Users.Where(u=>u.IsDeleted==false)
                                                        .OrderBy(u=>u.CreatedDate)
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
