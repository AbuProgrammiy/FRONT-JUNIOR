namespace FrontJunior.Domain.Entities.Models.OtherModels
{
    public class PasswordModel
    {
        public string PasswordHash { get; set; }
        public byte[] PassworSalt { get; set; }
    }
}
