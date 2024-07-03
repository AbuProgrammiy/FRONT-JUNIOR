using FrontJunior.Domain.Entities;

namespace FrontJunior.Application.Services.AuthServices
{
    public interface IAuthService
    {
        public string GenerateToken(User user);
    }
}
