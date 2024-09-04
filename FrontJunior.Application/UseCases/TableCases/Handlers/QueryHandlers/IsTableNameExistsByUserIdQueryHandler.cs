using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.QueryHandlers
{
    public class IsTableNameExistsByUserIdQueryHandler : IRequestHandler<IsTableNameExistsByUserIdQuery, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public IsTableNameExistsByUserIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(IsTableNameExistsByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.ActiveTables.FirstOrDefaultAsync(t => t.User.Id == request.UserId && t.Name == request.TableName)!=null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
