using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Domain.Entities.Views;
using FrontJunior.Domain.MainModels;
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
                User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u=> u.Id == request.Id);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "User not found!"
                    };
                }

                IEnumerable<Table> tables = await _applicationDbContext.Tables.Where(t => t.User == user).ToListAsync();

                foreach(Table table in tables)
                {
                    IEnumerable<DataStorage> dataStorages = await _applicationDbContext.DataStorage.Where(d => d.Table == table).ToListAsync();

                    await _applicationDbContext.DeletedDataStorage.AddRangeAsync(dataStorages);
                    _applicationDbContext.DataStorage.RemoveRange(dataStorages);

                    table.IsDeleted=true;
                    table.DeletedDate = DateTime.UtcNow;
                }

                await _applicationDbContext.DeletedTables.AddRangeAsync(tables);
                _applicationDbContext.Tables.RemoveRange(tables);

                user.IsDeleted=true;
                user.DeletedDate = DateTime.UtcNow;

                await _applicationDbContext.DeletedUsers.AddAsync(user);
                _applicationDbContext.Users.Remove(user);

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
