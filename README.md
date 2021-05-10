# Table of Context

- [Take Home Assignment Description](#Take-Home-Assignment-Description)

- [Implementation](#Implementation)

- [How to run](#How-to-run)

## Take Home Assignment Description

Please find below the information about the application that you’re going to build.

- Back-end Application
  - ASP.NET Core (version 3.1 or 5)
  - Authentication
  - Database persistence is optional (In-memory persistence or using SQL Server)
  - A product consists of 4 properties
    - Id
    - Name
    - Price
    - Description
- 5 REST APIs
  - Get product by ID
  - Get all products
  - Create a new product
  - Update an existing product
  - Delete a product
- Exception handling – please handle all internal exceptions and return an appropriate status codes with clear exception message back to user

## Implementation

1. It's a ASP.NET Core v5
2. EF Core In-Memory DB
3. it uses serilog package to output to console and also txt file in `%programdata%/tha/`
4. It uses simple authentication login and generate JWT token through **HTTP POST** `api/account` api
5. all API Authorization requires a simple simple bearer token header and uses Authorization Policy filter
6. Global error handling is implemented using `IApplicationBuilder` extension class called `ErrorHandlerMiddlewareExtension`

## How to run

1. to this app just `cd` to `THA_Api` folder and run `dotnet run`
2. to run unit test just cd to `Tests\THA.Test.API` and run `dotnet test`
3. you may inspect and tryout each API through swagger http://localhost:5000/swagger/index.html
