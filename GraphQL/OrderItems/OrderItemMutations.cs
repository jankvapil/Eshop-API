using System.Threading;
using System.Threading.Tasks;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;
using HotChocolate;
using HotChocolate.Types;

namespace Eshop.GraphQL.OrderItems
{
    [ExtendObjectType(Name = "Mutation")]
    public class OrderItemMutations
    {
      
        [UseApplicationDbContext]
        public async Task<AddOrderItemPayload> AddOrderItemAsync(
            AddOrderItemInput input,
            [ScopedService] ApplicationDbContext context)
        {
            var orderItem = new OrderItem
            {
                OrderId = input.OrderId,
                ProductId = input.ProductId,
                Count = input.Count
            };

            context.OrderItems.Add(orderItem);
            await context.SaveChangesAsync();

            return new AddOrderItemPayload(orderItem);
        }
    }
}