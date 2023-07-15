using Domain.Enums;
namespace Domain.Entities
{
    public class OrderEvent
    {

        public Guid Id { get; set; }
        public Guid OrderId { get; set; }

        public Order? Order { get; set; }

        public OrderStatus Status { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
