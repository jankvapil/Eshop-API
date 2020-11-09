namespace Eshop.GraphQL.OrderItems
{
    public record AddOrderItemInput(
        int OrderId,
        int ProductId,
        int Count
    );
}