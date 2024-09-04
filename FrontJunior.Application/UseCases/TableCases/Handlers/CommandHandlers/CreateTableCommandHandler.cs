using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Commands;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Views;
using FrontJunior.Domain.MainModels;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.CommandHandlers
{
    public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CreateTableCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ActiveUser user = await _applicationDbContext.ActiveUsers.FirstOrDefaultAsync(u => u.Id == request.UserId);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "User not found!"
                    };
                }

                ActiveTable table = await _applicationDbContext.ActiveTables.Where(t => t.User.Id == request.UserId).FirstOrDefaultAsync(t => t.Name == request.Name);

                if (table != null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "Table already exists!"
                    };
                }

                table =request.Adapt<ActiveTable>();
                table.CreatedDate=DateTime.UtcNow;
                table.User = user;

                await _applicationDbContext.ActiveTables.AddAsync(table);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                table = await _applicationDbContext.ActiveTables.FirstOrDefaultAsync(t => t.User == table.User && t.Name == table.Name);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Response = table.Id.ToString()
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
