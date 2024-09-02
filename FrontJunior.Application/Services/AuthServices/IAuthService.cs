using FrontJunior.Domain.Entities.Views;
using FrontJunior.Domain.MainModels;

namespace FrontJunior.Application.Services.AuthServices
{
    public interface IAuthService
    {
        public TokenModel GenerateToken(User user);
    }
}
