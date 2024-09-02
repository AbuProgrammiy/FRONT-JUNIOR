using System.Text.Json.Serialization;

namespace FrontJunior.Domain.MainModels
{
    public class Table
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        public string Name { get; set; }
        public byte ColumnCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
