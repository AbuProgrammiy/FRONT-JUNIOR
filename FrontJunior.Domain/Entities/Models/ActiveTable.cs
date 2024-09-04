using FrontJunior.Domain.MainModels;
using System.Text.Json.Serialization;

namespace FrontJunior.Domain.Entities.Models
{
    public class ActiveTable:Table
    {
        [JsonIgnore]
        public virtual ActiveUser User { get; set; }
    }
}
