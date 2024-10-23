using FrontJunior.Domain.Entities.Enums;

namespace FrontJunior.Domain.Entities.Models.PrimaryModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PassworSalt { get; set; }
        public Roles Role { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
