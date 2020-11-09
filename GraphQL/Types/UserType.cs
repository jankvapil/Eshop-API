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
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
        //   descriptor
        //     .AsNode()
        //     .IdField(t => t.Id)
        //     .NodeResolver((ctx, id) => ctx.DataLoader<UserByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));
              
          descriptor
            .Field(t => t.UserOrders)
            .ResolveWith<UserResolvers>(t => t.GetOrdersAsync(default!, default!, default!, default))
            // .UseDbContext<ApplicationDbContext>()
            .Name("orders");
        }

        private class UserResolvers
        {
            [UseApplicationDbContext]
            public async Task<IEnumerable<Order>> GetOrdersAsync(
                User user,
                [ScopedService] ApplicationDbContext dbContext,
                OrderByIdDataLoader orderById,
                CancellationToken cancellationToken)
            {
                int[] orderIds = await dbContext.Users
                    .Where(u => u.Id == user.Id)
                    .Include(u => u.UserOrders)
                    .SelectMany(u => u.UserOrders.Select(t => t.OrderId))
                    .ToArrayAsync();

                return await orderById.LoadAsync(orderIds, cancellationToken);
            }
        }
    }
}