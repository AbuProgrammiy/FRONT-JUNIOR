using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.QueryHandlers
{
    public class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetTableByIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Table table = await _applicationDbContext.Tables.FirstOrDefaultAsync(t => t.Id == request.Id);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = table
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
