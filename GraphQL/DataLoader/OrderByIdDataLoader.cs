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
    public class OrderByIdDataLoader : BatchDataLoader<int, Order>
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

        protected override async Task<IReadOnlyDictionary<int, Order>> LoadBatchAsync(
            IReadOnlyList<int> keys, 
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext = 
                _dbContextFactory.CreateDbContext();

            return await dbContext.Orders
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}