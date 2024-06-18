using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Application.UseCases.UserCases.Queries;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FrontJunior.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{page}/{count}")]
        public async Task<IEnumerable<User>> GetAll(int page,int count)
        {
            return await _mediator.Send(new GetAllUsersQuery { Page = page, Count = count });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<User> GetById(Guid id)
        {
            return await _mediator.Send(new GetUserByIdQuery { Id = id });
        }

        [HttpPost]
        public async Task<ResponseModel> Create(CreateUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        public async Task<ResponseModel> Update(UpdateUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseModel> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteUserCommand { Id = id });
        }
    }
}
