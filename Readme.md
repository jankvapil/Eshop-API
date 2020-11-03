
# GraphQL API

Ukázkový projekt, vytvořený na základě oficiálního [workshopu](https://github.com/ChilliCream/graphql-workshop).

Příkazy pro vytvoření databáze

```console
dotnet build GraphQL
dotnet ef migrations add Initial --project GraphQL
dotnet ef database update --project GraphQL
```

Spuštění aplikace

```console
dotnet run --project GraphQL
```

## USER

Vytvoření uživatele.

### Mutace 
```graphql
mutation AddUser {
  addUser(input: {
    name: "User"
    email: "user@email.com"
  }) {
    user {
      id
    }
  }
}
```

### Query 

Paralelní vykonávání dotazů.

```graphql
query GetUserNamesInParallel {
  a: users {
    name
  }
  b: users {
    name
  }
  c: users {
    name
  }
}
```

Výběr uživatele podle id.

```graphql
query {
  user(id: "VXNlcgppMQ==") {
    name
    email
  }
}
```

Vnořené subquery.

```graphql
query {
  users {
    name
    orders {
      orderDate
    }
  }
}
```

## ORDER 
### Mutace 

```graphql
mutation Addorder {
  addOrder(input: {
    orderDate:  "2020-10-14 12:00:00.0"
  }) {
   order {
     id
   }
  }
}

```

### Query

```graphql
query {
  orders {
    id
    orderDate
  }
}
```