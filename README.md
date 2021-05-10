# Take Home Assignment
## Table of content
  - [Assignment Description](#assignment-description)
  - [Implementation](#implementation)
  - [How this App DB Context work](#how-this-app-db-context-work)
  - [How to run](#how-to-run)
## Assignment Description

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

## How this App DB Context work
1. There is `User` and `Product` DBContext
2. Delete entity will not physically deleted it, instead it will only set its `STATUS` field to `Statuses.DELETE`. 
3. Get entity/entities will return all that are not `Statuses.DELETE`. a way of making the deleted record.
4. User are assigned with Roles of `PRODUCT_ADMIN` or `USER`
5. `PRODUCT_ADMIN` role can Add and Delete product
6. `USER` role can get one or all products
7. Authorization Policy will use the role as well.
8. To prevent conflicting update or out of date update, i use `EDIT_DATE` as concurrency token field to check if user tries to update out of date entity and return proper status code 409 accordingly
9. `THA.DBInit.csproj` are use to populate sample Users and Products into in memory database with test/mock data for immediate API invocation once the application is started.
## How to run

1. to this app just `cd` to `THA_Api` folder and run `dotnet run`
2. to run unit test just cd to `Tests\THA.Test.API` and run `dotnet test`
3. you may inspect and tryout each API through swagger http://localhost:5000/swagger/index.html
