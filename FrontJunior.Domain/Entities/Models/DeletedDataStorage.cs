using FrontJunior.Domain.MainModels;
using System.Text.Json.Serialization;

namespace FrontJunior.Domain.Entities.Models
{
    public class DeletedDataStorage:DataStorage
    {
        [JsonIgnore]
        public DeletedTable Table { get; set; }
    }
}
