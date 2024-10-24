using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.QueryHandlers
{
    public class GetAllTablesQueryHandler : IRequestHandler<GetAllTableQuery, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllTablesQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(GetAllTableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Table> tables = await _applicationDbContext.Tables.OrderBy(t => t.CreatedDate)
                                                                              .Skip((request.Page - 1) * request.Count)
                                                                              .Take(request.Count)
                                                                              .ToListAsync(cancellationToken);

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
