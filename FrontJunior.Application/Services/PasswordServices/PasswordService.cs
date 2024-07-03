using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace FrontJunior.Application.Services.PasswordServices
{
    public class PasswordService : IPasswordService
    {
        public bool CheckPassword(string password, PasswordModel passwordModel)
        {
            string PasswordHash = Convert.ToHexString(
                Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(password),
                    passwordModel.PassworSalt,
                    350000,
                    HashAlgorithmName.SHA512,
                    64));

            return PasswordHash.Equals(passwordModel.PasswordHash);
        }

        public PasswordModel HashPassword(string password)
        {
            PasswordModel passwordModel = new PasswordModel();

            passwordModel.PassworSalt = RandomNumberGenerator.GetBytes(64);
            passwordModel.PasswordHash = Convert.ToHexString(
                Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(password),
                    passwordModel.PassworSalt,
                    350000,
                    HashAlgorithmName.SHA512,
                    64));

            return passwordModel;
        }
    }
}
