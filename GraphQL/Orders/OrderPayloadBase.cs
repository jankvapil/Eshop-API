using System.Collections.Generic;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;

namespace Eshop.GraphQL.Orders
{
    public class OrderPayloadBase : Payload
    {
        protected OrderPayloadBase(Order order)
        {
            Order = order;
        }

        protected OrderPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Order? Order { get; init; }
    }
}