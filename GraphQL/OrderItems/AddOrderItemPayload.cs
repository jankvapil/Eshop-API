using System.Collections.Generic;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;

namespace Eshop.GraphQL.OrderItems
{
    public class AddOrderItemPayload : OrderItemPayloadBase
    {
        public AddOrderItemPayload(OrderItem orderItem)
             : base(orderItem)
         {
         }

         public AddOrderItemPayload(IReadOnlyList<UserError> errors)
             : base(errors)
         {
         }
    }
}