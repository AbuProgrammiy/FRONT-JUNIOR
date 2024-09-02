namespace FrontJunior.Domain.Entities.Views
{
    public class PasswordModel
    {
        public string PasswordHash { get; set; }
        public byte[] PassworSalt { get; set; }
    }
}
