namespace Eshop.GraphQL.Data
{
    public class UserOrder
    {
        public System.Guid UserId { get; set; }

        public User? User { get; set; }

        public System.Guid OrderId { get; set; }

        public Order? Order { get; set; }
    }
}