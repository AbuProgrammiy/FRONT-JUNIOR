using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.CRUDCase.Commands;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace FrontJunior.Application.UseCases.CRUDCase.Handlers.CommandHandlers
{
    public class CreateComandHandler : IRequestHandler<CreateCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CreateComandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ResponseModel> Handle(CreateCommand request, CancellationToken cancellationToken)
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

                DataStorage columns=await _applicationDbContext.DataStorage.Where(d=>d.IsData==false).FirstOrDefaultAsync(d=>d.Table==table);

                DataStorage dataStorage = new DataStorage
                {
                    Table = table,
                    IsData = true,
                };

                JObject body = JObject.Parse(request.Body.ToString());

                PropertyInfo[] properties = dataStorage.GetType().GetProperties();

                string value;

                for (byte i=0;i<table.ColumnCount;i++)
                {
                    value = body.GetValue(columns.GetType().GetProperty(properties[i].Name).GetValue(columns).ToString()).ToString();

                    if (value != null)
                    {
                        properties[i].SetValue(dataStorage, value);
                    }
                }

                await _applicationDbContext.DataStorage.AddAsync(dataStorage);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Message = "Data successfully created!"
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
