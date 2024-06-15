namespace FrontJunior.Domain.Entities
{
    public class Table
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public byte ColumnCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
