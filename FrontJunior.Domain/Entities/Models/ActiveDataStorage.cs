using FrontJunior.Domain.MainModels;
using System.Text.Json.Serialization;

namespace FrontJunior.Domain.Entities.Models
{
    public class ActiveDataStorage:DataStorage
    {
        [JsonIgnore]
        public ActiveTable Table { get; set; }
    }
}
