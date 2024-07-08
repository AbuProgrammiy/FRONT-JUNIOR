using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;

namespace FrontJunior.Application.Services.AuthServices
{
    public interface IAuthService
    {
        public TokenModel GenerateToken(User user);
    }
}
