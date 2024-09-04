using FrontJunior.Domain.MainModels;
using System.Text.Json.Serialization;

namespace FrontJunior.Domain.Entities.Models
{
    public class DeletedTable:Table
    {
        [JsonIgnore]
        public virtual DeletedUser User { get; set; }

        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
