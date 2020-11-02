using System.Threading;
using System.Threading.Tasks;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;
using HotChocolate;
using HotChocolate.Types;

namespace Eshop.GraphQL.Orders
{
    [ExtendObjectType(Name = "Mutation")]
    public class OrderMutations
    {
      
        [UseApplicationDbContext]
        public async Task<AddOrderPayload> AddOrderAsync(
            AddOrderInput input,
            [ScopedService] ApplicationDbContext context)
        {
            var order = new Order
            {
                OrderDate = input.OrderDate
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            return new AddOrderPayload(order);
        }
    }
}