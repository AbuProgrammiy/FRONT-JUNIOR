using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.CRUDCases.Commands;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace FrontJunior.Application.UseCases.CRUDCases.Handlers.CommandHandlers
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

                JObject body = JObject.Parse(request.Body.ToString());

                if (body.Properties().Count() == 0)
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Response = "Body is not filled!"
                    };
                }

                DataStorage columns = await _applicationDbContext.DataStorage.FirstOrDefaultAsync(d => d.Table == table && d.IsData == false);

                DataStorage dataStorage = new DataStorage
                {
                    Table = table,
                    IsData = true,
                };

                PropertyInfo[] properties = dataStorage.GetType().GetProperties();

                string value;

                for (byte i=0;i<table.ColumnCount;i++)
                {
                    value = body.GetValue(columns.GetType().GetProperty(properties[i].Name).GetValue(columns).ToString())?.ToString();

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
                    Response = "Data successfully created!"
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
