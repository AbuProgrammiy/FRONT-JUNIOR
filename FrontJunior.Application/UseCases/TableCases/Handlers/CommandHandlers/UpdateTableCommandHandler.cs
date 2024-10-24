﻿using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.TableCases.Commands;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.TableCases.Handlers.CommandHandlers
{
    public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UpdateTableCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Table table = await _applicationDbContext.Tables.FirstOrDefaultAsync(t => t.Id == request.Id);

                if (table == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Table not found!"
                    };
                }

                table = request.Adapt<Table>();

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = "Table updated successfuly!"
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
