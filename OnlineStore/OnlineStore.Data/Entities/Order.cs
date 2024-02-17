using OnlineStore.DTO.Models;

namespace OnlineStore.Data.Entities
{
    public class Order : IBaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
