using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.CRUDCases.Commands;
using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace FrontJunior.Application.UseCases.CRUDCases.Handlers.CommandHandlers
{
    public class UpdateByAnyCommandHandler : IRequestHandler<UpdateByAnyCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UpdateByAnyCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(UpdateByAnyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ActiveUser user = await _applicationDbContext.ActiveUsers.FirstOrDefaultAsync(u => u.SecurityKey == request.SecurityKey);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "User not found!"
                    };
                }

                ActiveTable table = await _applicationDbContext.ActiveTables.Where(t => t.User == user).FirstOrDefaultAsync(t => t.Name == request.TableName);

                if (table == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Table not found!"
                    };
                }

                ActiveDataStorage columns = await _applicationDbContext.ActiveDataStorage.Where(d => d.IsData == false).FirstOrDefaultAsync(d => d.Table == table);

                PropertyInfo[] properties = columns.GetType().GetProperties();

                PropertyInfo property=null;

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
                        Response = "Column of data not found to update!"
                    };
                }

                ActiveDataStorage dataStorage = _applicationDbContext.ActiveDataStorage.AsEnumerable()
                                                                           .Where(d =>d.Table==table && d.IsData == true)
                                                                           .FirstOrDefault(d => d.GetType()
                                                                                                           .GetProperty(property.Name)
                                                                                                           .GetValue(d)?.ToString() == request.ColumnValue);

                if (dataStorage == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Response = "Data not found to update!"
                    };
                }

                JObject body = JObject.Parse(request.Body.ToString());

                string value;

                for (byte i = 0; i < table.ColumnCount; i++)
                {
                    value = body.GetValue(columns.GetType().GetProperty(properties[i].Name).GetValue(columns).ToString())?.ToString();

                    if (value != null)
                    {
                        properties[i].SetValue(dataStorage, value);
                    }
                }

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Response = "Data successfuly updated!"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Response = $"Something went wrong!: {ex.Message}"
                };
            }
        }
    }
}
