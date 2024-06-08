namespace FrontJunior.Domain.Entities
{
    public class Table
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public byte ColumnCount { get; set; }
    }
}
