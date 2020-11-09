using System.Collections.Generic;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;

namespace Eshop.GraphQL.OrderItems
{
    public class OrderItemPayloadBase : Payload
    {
        protected OrderItemPayloadBase(OrderItem orderItem)
        {
            OrderItem = orderItem;
        }

        protected OrderItemPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public OrderItem? OrderItem { get; init; }
    }
}