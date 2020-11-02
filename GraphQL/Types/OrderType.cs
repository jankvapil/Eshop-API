// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using Eshop.GraphQL.Data;
// using Eshop.GraphQL.DataLoader;
// using HotChocolate;
// using HotChocolate.Types;

// namespace Eshop.GraphQL.Types
// {
//     public class OrderType : ObjectType<Order>
//     {
//         protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
//         {
//             descriptor
//                 .Field(t => t.UserOrders)
//                 .ResolveWith<OrderResolvers>(t => t.GetUsersAsync(default!, default!, default!, default))
//                 .UseDbContext<ApplicationDbContext>()
//                 .Name("users");
//         }

//         private class OrderResolvers
//         {
//             public async Task<IEnumerable<User>> GetUsersAsync(
//                 Order order,
//                 [ScopedService] ApplicationDbContext dbContext,
//                 UserByIdDataLoader userById,
//                 CancellationToken cancellationToken)
//             {
//                 int[] orderIds = await dbContext.Orders
//                     .Where(o => o.Id == order.Id)
//                     .Include(o => o.UserOrders)
//                     .SelectMany(o => o.UserOrders.Select(t => t.UserId))
//                     .ToArrayAsync();

//                 return await userById.LoadAsync(orderIds, cancellationToken);
//             }
//         }
//     }
// }