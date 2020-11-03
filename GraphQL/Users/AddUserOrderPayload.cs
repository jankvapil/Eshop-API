using System.Collections.Generic;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;

namespace Eshop.GraphQL.Users
{
    public class AddUserOrderPayload : Payload
    {
        public AddUserOrderPayload(User user)
        {
            User = user;
        }

        public AddUserOrderPayload(UserError error)
            : base(new[] { error })
        {
        }

        public User? User { get; init; }
    }
}