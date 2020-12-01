namespace Eshop.GraphQL.Users
{
    public record AddUserInput(
        string Name,
        string Email,
        string Password,
        string Address
    );
}