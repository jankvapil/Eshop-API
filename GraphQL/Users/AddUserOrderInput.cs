using System;
using System.Collections.Generic;
using Eshop.GraphQL.Data;
using HotChocolate.Types.Relay;

namespace Eshop.GraphQL.Users
{
    public record AddUserOrderInput(
        [ID(nameof(User))]
        System.Guid UserId,
        [ID(nameof(Order))]
        IReadOnlyList<Guid> OrderIds);
}