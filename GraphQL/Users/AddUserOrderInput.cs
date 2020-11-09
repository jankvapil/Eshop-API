using System;
using System.Collections.Generic;
using Eshop.GraphQL.Data;
using HotChocolate.Types.Relay;

namespace Eshop.GraphQL.Users
{
    public record AddUserOrderInput(
        int UserId,
        IReadOnlyList<int> OrderIds);
}