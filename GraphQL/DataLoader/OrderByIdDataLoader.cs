using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Eshop.GraphQL.Data;
using GreenDonut;
using HotChocolate.DataLoader;

namespace Eshop.GraphQL.DataLoader
{
    public class OrderByIdDataLoader : BatchDataLoader<System.Guid, Order>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public OrderByIdDataLoader(
            IBatchScheduler batchScheduler, 
            IDbContextFactory<ApplicationDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ?? 
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<System.Guid, Order>> LoadBatchAsync(
            IReadOnlyList<System.Guid> keys, 
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext = 
                _dbContextFactory.CreateDbContext();

            return await dbContext.Orders
                .Where(o => keys.Contains(o.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}