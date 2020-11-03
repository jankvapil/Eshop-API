namespace Eshop.GraphQL.Data
{
    public class UserOrder
    {
        public int UserId { get; set; }

        public User? User { get; set; }

        public int OrderId { get; set; }

        public Order? Order { get; set; }
    }
}