using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.QueryHandlers
{
    public class IsTableNameExistsByUserIdQueryHandler : IRequestHandler<IsTableNameExistsByUserIdQuery, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public IsTableNameExistsByUserIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(IsTableNameExistsByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Table table = await _applicationDbContext.Tables.FirstOrDefaultAsync(t => t.User.Id == request.UserId && t.Name == request.TableName);

                if (table != null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "Table already exists!"
                    };
                }
                else
                {
                    return new ResponseModel
                    {
                        IsSuccess = true,
                        StatusCode = 200,
                        Response = "Table is not exists!"
                    };
                }
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
