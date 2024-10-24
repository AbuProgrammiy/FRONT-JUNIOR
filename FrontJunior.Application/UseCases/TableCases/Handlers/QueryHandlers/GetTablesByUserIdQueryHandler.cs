using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.QueryHandlers
{
    public class GetTablesByUserIdQueryHandler : IRequestHandler<GetTablesByUserIdQuery, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetTablesByUserIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(GetTablesByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Table> tables = await _applicationDbContext.Tables.Where(t => t.User.Id == request.UserId).ToListAsync();


                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = tables
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
