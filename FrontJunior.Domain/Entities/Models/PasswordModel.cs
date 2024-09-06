namespace FrontJunior.Domain.Entities.Models
{
    public class PasswordModel
    {
        public string PasswordHash { get; set; }
        public byte[] PassworSalt { get; set; }
    }
}
