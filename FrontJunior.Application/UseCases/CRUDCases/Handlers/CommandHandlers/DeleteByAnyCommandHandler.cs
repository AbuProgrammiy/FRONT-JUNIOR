using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.CRUDCases.Commands;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace FrontJunior.Application.UseCases.CRUDCases.Handlers.CommandHandlers
{
    public class DeleteByAnyCommandHandler : IRequestHandler<DeleteByAnyCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteByAnyCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(DeleteByAnyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "User not found!"
                    };
                }

                Table table = await _applicationDbContext.Tables.FirstOrDefaultAsync(t => t.User == user && t.Name == request.TableName);

                if (table == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Table not found!"
                    };
                }

                DataStorage columns = await _applicationDbContext.DataStorage.Where(d => d.IsData == false).FirstOrDefaultAsync(d => d.Table == table);

                PropertyInfo[] properties = columns.GetType().GetProperties();

                PropertyInfo property = null;

                for (byte i = 0; i < table.ColumnCount; i++)
                {
                    property = columns.GetType()
                                      .GetProperty(properties[i].Name)
                                      .GetValue(columns).ToString() == request.ColumnName ? properties[i] : null;

                    if (property != null)
                    {
                        break;
                    }
                }

                if (property == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Column of data not found to delete!"
                    };
                }

                DataStorage dataStorage = _applicationDbContext.DataStorage.AsEnumerable().Where(d =>d.Table==table && d.IsData == true)
                                                                                          .FirstOrDefault(d => d.GetType()
                                                                                                                         .GetProperty(property.Name)
                                                                                                                         .GetValue(d)?.ToString() == request.ColumnValue);

                if (dataStorage == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Data not found to delete!"
                    };
                }

                _applicationDbContext.DataStorage.Remove(dataStorage);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = "Data successfuly deleted!"
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
