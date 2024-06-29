using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.CRUDCase.Queries;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FrontJunior.Application.UseCases.CRUDCase.Handlers.QueryHandlers
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, object>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<object> Handle(GetAllQuery request, CancellationToken cancellationToken)
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

                List<DataStorage> dataStorages= await _applicationDbContext.DataStorage.Where(d=>d.IsData==true).Skip((request.Page-1)*request.Count).Take(request.Count).ToListAsync();

                if (dataStorages == null)
                {
                    return null;
                }

                List<Dictionary<string, string>> datas = new List<Dictionary<string, string>>();
                Dictionary<string, string> data = new Dictionary<string, string>();

                PropertyInfo[] properties = columns.GetType().GetProperties();

                for(int i=0;i<dataStorages.Count;i++)
                {
                    for(byte j=0;j<table.ColumnCount;j++)
                    {
                        data.Add(properties[j].GetValue(columns).ToString(), properties[j].GetValue(dataStorages[i]).ToString());
                    }
                    datas.Add(data);
                }

                return datas;
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
