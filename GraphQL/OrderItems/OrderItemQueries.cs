using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Eshop.GraphQL.Data;
using Eshop.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace Eshop.GraphQL.OrderItems
{
    [ExtendObjectType(Name = "Query")]
    public class OrderItemQueries
    {
        [UseApplicationDbContext]
        public Task<List<OrderItem>> GetOrderItems([ScopedService] ApplicationDbContext context) =>
            context.OrderItems.ToListAsync();


        public Task<OrderItem> GetOrderItemAsync(
            int id,
            OrderItemByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);
    }
}