using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.CRUDCase.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FrontJunior.Application.UseCases.CRUDCase.Handlers.CommandHandlers
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(DeleteCommand request, CancellationToken cancellationToken)
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
                                      .GetValue(columns) == request.ColumnName ? properties[i] : null;

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
                        Message = "Column of data not found to delete!"
                    };
                }

                DataStorage dataStorage = await _applicationDbContext.DataStorage.Where(d => d.IsData == true)
                                                                                 .FirstOrDefaultAsync(d => d.GetType()
                                                                                                                     .GetProperty(property.Name)
                                                                                                                     .GetValue(d) == request.ColumnValue);

                if (dataStorage != null)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Data not found to delete!"
                    };
                }

                _applicationDbContext.DataStorage.Remove(dataStorage);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Data successfuly deleted!"
                };
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
