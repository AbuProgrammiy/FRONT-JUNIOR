namespace FrontJunior.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PassworSalt { get; set; }
        public string Role { get; set; }
        public string SecurityKey { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
