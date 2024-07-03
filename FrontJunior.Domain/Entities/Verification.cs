namespace FrontJunior.Domain.Entities
{
    public class Verification
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string SentPassword { get; set; }
    }
}
