using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Views;
using FrontJunior.Domain.MainModels;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.CommandHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteUserCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ActiveUser user = await _applicationDbContext.ActiveUsers.FirstOrDefaultAsync(u=> u.Id == request.Id);
                DeletedUser deletedUser = user.Adapt<DeletedUser>();

                if (user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "User not found!"
                    };
                }

                List<ActiveTable> tables = await _applicationDbContext.ActiveTables.Where(t => t.User == user).ToListAsync();
                List<DeletedTable> deletedTables = tables.Adapt<List<DeletedTable>>();

                for(int i=0;i<tables.Count;i++)
                {
                    IEnumerable<ActiveDataStorage> dataStorages = await _applicationDbContext.ActiveDataStorage.Where(d => d.Table == tables[i]).ToListAsync();

                    await _applicationDbContext.DeletedDataStorage.AddRangeAsync(dataStorages.Adapt<IEnumerable<DeletedDataStorage>>());
                    _applicationDbContext.ActiveDataStorage.RemoveRange(dataStorages);

                    deletedTables[i].IsDeleted=true;
                    deletedTables[i].DeletedDate = DateTime.UtcNow;
                }

                await _applicationDbContext.DeletedTables.AddRangeAsync(deletedTables);
                _applicationDbContext.ActiveTables.RemoveRange(tables);

                deletedUser.IsDeleted=true;
                deletedUser.DeletedDate = DateTime.UtcNow;

                await _applicationDbContext.DeletedUsers.AddAsync(deletedUser);
                _applicationDbContext.ActiveUsers.Remove(user);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = "User deleted successfully!"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Response = $"Something went wrong: {ex.Message}"
                };
            }
        }
    }
}
