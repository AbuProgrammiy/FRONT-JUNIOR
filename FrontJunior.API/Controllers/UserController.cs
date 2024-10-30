using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Application.UseCases.UserCases.Queries;
using FrontJunior.Domain.Entities.Views;
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
        public async Task<ResponseModel> GetAll(int page,int count)
        {
            return await _mediator.Send(new GetAllUsersQuery { Page = page, Count = count });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseModel> GetById(Guid id)
        {
            return await _mediator.Send(new GetUserByIdQuery { Id = id });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseModel> IsUserExists(Guid id)
        {
            return await _mediator.Send(new IsUserExistsQuery { Id = id });
        }

        [HttpPost]
        public async Task<ResponseModel> SendVerificationToUser(SendVerificationToUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<object> Register(RegisterUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<object> LogIn(LogUserInCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<object> ResetPassword(ResetUserPasswordCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPatch]
        public async Task<ResponseModel> UpdateUsername(UpdateUserUsernameCommand request)
        {
            return await _mediator.Send(request);
        }


        [HttpPatch]
        public async Task<ResponseModel> UpdateEmail(UpdateUserEmailCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPatch]
        public async Task<ResponseModel> UpdatePassword(UpdateUserPasswordCommand request)
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
