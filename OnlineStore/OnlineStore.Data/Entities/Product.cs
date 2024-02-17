using OnlineStore.DTO.Models;


namespace OnlineStore.Data.Entities
{
    public class Product : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<Сategory>? Categories { get; set; }
    }
}
