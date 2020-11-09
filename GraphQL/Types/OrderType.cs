using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Eshop.GraphQL.Data;
using Eshop.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Resolvers;

namespace Eshop.GraphQL.Types
{
    public class OrderType : ObjectType<Order>
    {
        protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
        {
            descriptor
                .Field(t => t.UserOrders)
                .ResolveWith<OrderResolvers>(t => t.GetUsersAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("users");

            descriptor
                .Field(t => t.OrderItems)
                .ResolveWith<OrderResolvers>(t => t.GetOrderItemsAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("orderItems");
        }

        private class OrderResolvers
        {
            public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(
                Order order,
                [ScopedService] ApplicationDbContext dbContext,
                OrderItemByIdDataLoader orderItemById,
                CancellationToken cancellationToken)
            {
                int[] Ids = await dbContext.Orders
                    .Where(o => o.Id == order.Id)
                    .Include(o => o.OrderItems)
                    .SelectMany(oi => oi.OrderItems.Select(t => t.Id))
                    .ToArrayAsync();

                return await orderItemById.LoadAsync(Ids, cancellationToken);
            }

            public async Task<IEnumerable<User>> GetUsersAsync(
                Order order,
                [ScopedService] ApplicationDbContext dbContext,
                UserByIdDataLoader userById,
                CancellationToken cancellationToken)
            {
                int[] Ids = await dbContext.Orders
                    .Where(o => o.Id == order.Id)
                    .Include(o => o.UserOrders)
                    .SelectMany(o => o.UserOrders.Select(t => t.UserId))
                    .ToArrayAsync();

                return await userById.LoadAsync(Ids, cancellationToken);
            }
        }
    }
}