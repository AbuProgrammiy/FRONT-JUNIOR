﻿using FrontJunior.Application.Abstractions;
using FrontJunior.Application.UseCases.UserCases.Queries;
using FrontJunior.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontJunior.Application.UseCases.UserCases.Handlers.QueryHandlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetAllUsersQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationDbContext.Users.Where(u=>u.IsDeleted==false)
                                                        .Skip(request.Page)
                                                        .Take(request.Count)
                                                        .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
