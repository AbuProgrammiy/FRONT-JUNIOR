using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
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
                Table table = await _applicationDbContext.Tables.Where(t => t.User.Id == request.UserId&&t.IsDeleted==false).FirstOrDefaultAsync(t => t.Name == request.Name);

                if (table != null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "Table already exists!"
                    };
                }

                User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "User not found!"
                    };
                }

                table =request.Adapt<Table>();
                table.CreatedDate=DateTime.UtcNow;
                table.IsDeleted = false;
                table.User = user;

                await _applicationDbContext.Tables.AddAsync(table);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Message = "Table is succesfully created!"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = $"Something went wrong: {ex.Message}"
                };
            }
        }
    }
}
