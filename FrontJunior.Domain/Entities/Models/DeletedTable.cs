using FrontJunior.Domain.MainModels;

namespace FrontJunior.Domain.Entities.Models
{
    public class DeletedTable:Table
    {
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
