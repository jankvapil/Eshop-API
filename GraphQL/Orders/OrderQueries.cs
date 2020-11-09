using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Eshop.GraphQL.Data;
using Eshop.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace Eshop.GraphQL.Orders
{
    [ExtendObjectType(Name = "Query")]
    public class OrderQueries
    {
        [UseApplicationDbContext]
        public Task<List<Order>> GetOrders([ScopedService] ApplicationDbContext context) =>
            context.Orders.ToListAsync();


        public Task<Order> GetOrderAsync(
            // [ID(nameof(Order))]int id,
            int id,
            OrderByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);
    }
}