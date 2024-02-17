using OnlineStore.DTO.Models;

namespace OnlineStore.Data.Entities
{
    public class Сategory : IBaseEntity
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
