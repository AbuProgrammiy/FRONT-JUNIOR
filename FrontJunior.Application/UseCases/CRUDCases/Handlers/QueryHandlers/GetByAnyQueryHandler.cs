using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.CRUDCases.Queries;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FrontJunior.Application.UseCases.CRUDCases.Handlers.QueryHandlers
{
    public class GetByAnyQueryHandler : IRequestHandler<GetByAnyQuery, object>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetByAnyQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<object> Handle(GetByAnyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.SecurityKey == request.SecurityKey);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "User not found!"
                    };
                }

                Table table = await _applicationDbContext.Tables.Where(t => t.User == user).FirstOrDefaultAsync(t => t.Name == request.TableName);

                if (table == null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Table not found!"
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
                        Message = "Column of data not found to update!"
                    };
                }

                DataStorage dataStorage = _applicationDbContext.DataStorage.AsEnumerable()
                                                                           .Where(d => d.IsData == true)
                                                                           .FirstOrDefault(d => d.GetType()
                                                                                                           .GetProperty(property.Name)
                                                                                                           .GetValue(d).ToString() == request.ColumnValue);

                if (dataStorage == null)
                {
                    return null;
                }

                Dictionary<string,string> data= new Dictionary<string,string>();

                for(byte i = 0;i < table.ColumnCount;i++)
                {
                    data.Add(properties[i].GetValue(columns)?.ToString(), properties[i].GetValue(dataStorage)?.ToString());
                }

                return data;
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = $"Something went wrong!: {ex.Message}"
                };
            }
        }
    }
}
