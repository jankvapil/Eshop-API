namespace Eshop.GraphQL.Products
{
    public record AddProductInput(
        string Name,
        float Price,
        string Type,
        string Description,
        string ImgUrl
    );
}