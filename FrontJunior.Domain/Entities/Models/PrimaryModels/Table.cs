using System.Text.Json.Serialization;

namespace FrontJunior.Domain.Entities.Models.PrimaryModels
{
    public class Table
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Stars { get; set; }
        public byte ColumnCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
