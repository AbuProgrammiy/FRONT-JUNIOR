using FrontJunior.Domain.Entities.Models;
using FrontJunior.Domain.Entities.Models.PrimaryModels;

namespace FrontJunior.Application.Services.AuthServices
{
    public interface IAuthService
    {
        public string GenerateToken(User user);
    }
}
