using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.DTOs;
using Mapster;
using MediatR;

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
                if(_applicationDbContext.Tables.Where(t=>t.User.Id==request.UserId).FirstOrDefault(t=>t.Name==request.Name) != null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "Table already exists!"
                    };
                }

                Table table=request.Adapt<Table>();
                table.IsDeleted = false;
                table.User = _applicationDbContext.Users.FirstOrDefault(u => u.Id == request.UserId);

                await _applicationDbContext.Tables.AddAsync(table);
                _applicationDbContext.SaveChangesAsync(cancellationToken);

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
