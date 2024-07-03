namespace FrontJunior.Domain.Entities.DTOs
{
    public class EmailDTO
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHTML { get; set; }
    }
}
