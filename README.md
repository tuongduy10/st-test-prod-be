# Product Management Backend API Documentation

## 1. Overview

This project is a backend API for a retail/e-commerce product management system.

The system is designed using:

* .NET 8
* PostgreSQL
* Entity Framework Core
* Clean Architecture
* Domain-Driven Design (DDD)
* Minimal APIs + Carter
* FluentValidation
* Generic Repository Pattern
* Unit Of Work Pattern

The goal of this assessment is to build scalable and maintainable product management endpoints with strong consistency and clean architecture.

---

# 2. Approach to the Requirement

## Development Process

The implementation process follows these steps:

1. Analyze business requirements.
2. Identify core domain entities and aggregates.
3. Design database schema.
4. Define RESTful APIs.
5. Implement Clean Architecture layers.
6. Implement domain logic.
7. Implement validation.
8. Implement pagination and edge-case handling.
9. Implement concurrency handling.
10. Prepare API documentation and Postman collection.

---

# 3. Architecture Design

## Architecture Style

The project uses:

* Clean Architecture
* Domain-Driven Design (DDD)
* CQRS-lite approach
* Feature-based organization

Benefits:

* Separation of concerns
* Maintainability
* Scalability
* Testability
* Easier future migration to microservices

---

# 4. Project Structure

```txt
src/
 ├── Api/
 │    ├── Endpoints/
 │    ├── Filters/
 │    ├── Middlewares/
 │    └── Extensions/
 │
 ├── Application/
 │    ├── Common/
 │    ├── Products/
 │    │    ├── Commands/
 │    │    ├── Queries/
 │    │    ├── DTOs/
 │    │    ├── Validators/
 │    │    └── Services/
 │
 ├── Domain/
 │    ├── Common/
 │    ├── Entities/
 │    ├── Enums/
 │    ├── Exceptions/
 │    └── Repositories/
 │
 ├── Infrastructure/
 │    ├── Persistence/
 │    ├── Repositories/
 │    ├── Configurations/
 │    └── DependencyInjection/
 │
 └── Shared/
```

---

# 5. Database Design

## Database Choice

The project uses PostgreSQL.

### Why PostgreSQL?

PostgreSQL was selected because:

* Strong ACID compliance
* Excellent concurrency handling
* Reliable transaction support
* High scalability
* Mature indexing capabilities
* Strong support with EF Core
* Good performance for relational e-commerce systems

A relational database is preferred because product management requires:

* consistency
* transactions
* relationships
* constraints
* indexing

---

# 6. Main Entities

## Product

Represents a sellable product.

### Fields

| Field       | Type     |
| ----------- | -------- |
| Id          | Guid     |
| Name        | string   |
| Slug        | string   |
| Description | string   |
| Price       | decimal  |
| Status      | enum     |
| CreatedAt   | datetime |
| UpdatedAt   | datetime |
| IsDeleted   | bool     |

---

## ProductVariant

Represents product variations.

### Fields

| Field         | Type   |
| ------------- | ------ |
| Id            | Guid   |
| SKU           | string |
| StockQuantity | int    |
| Size          | string |
| Color         | string |

---

# 7. Database Relationships

```txt
Product
   └── ProductVariants (1:N)
```

Each product can contain multiple variants.

---

# 8. Database Constraints

## Unique Constraints

* Product.Slug
* ProductVariant.SKU

## Validation Constraints

* Price > 0
* StockQuantity >= 0

## Soft Delete

Global query filters are used:

```csharp
builder.HasQueryFilter(x => !x.IsDeleted);
```

---

# 9. Concurrency Handling

The system uses PostgreSQL xmin concurrency token.

```csharp
builder.UseXminAsConcurrencyToken();
```

This helps prevent:

* lost updates
* dirty writes
* concurrency conflicts

Benefits:

* optimistic concurrency control
* lightweight solution
* built-in PostgreSQL feature

---

# 10. Technology Stack Components

| Component             | Technology            |
| --------------------- | --------------------- |
| Framework             | .NET 8                |
| ORM                   | Entity Framework Core |
| Database              | PostgreSQL            |
| API Style             | Minimal APIs          |
| Endpoint Organization | Carter                |
| Validation            | FluentValidation      |
| Architecture          | Clean Architecture    |
| Design Style          | DDD                   |
| API Documentation     | Swagger               |
| Dependency Injection  | Built-in .NET DI      |

---

# 11. Domain-Driven Design (DDD)

## Aggregate Root

Product is implemented as Aggregate Root.

Business rules are enforced inside the domain model.

Examples:

* duplicate SKU prevention
* invalid stock prevention
* invalid price prevention

---

# 12. Repository Pattern

The project uses separated repositories:

* Read Repository
* Command Repository

Benefits:

* cleaner abstraction
* separation of responsibilities
* better scalability
* easier future CQRS migration

---

# 13. Unit Of Work

Unit Of Work is used to ensure:

* transactional consistency
* atomic operations
* controlled SaveChanges execution

---

# 14. API Design

The API follows RESTful conventions.

Base Route:

```txt
/api/products
```

---

# 15. Implemented APIs

## 1. Get Product Paging

### Endpoint

```http
GET /api/products?pageIndex=1&pageSize=10
```

### Features

* server-side pagination
* sorting
* efficient querying
* AsNoTracking optimization

---

## 2. Create Product

### Endpoint

```http
POST /api/products
```

### Request Body

```json
{
  "name": "Nike Shirt",
  "slug": "nike-shirt",
  "description": "Sport shirt",
  "price": 50
}
```

---

## 3. Update Product

### Endpoint

```http
PUT /api/products/{id}
```

---

## 4. Soft Delete Product

### Endpoint

```http
DELETE /api/products/{id}
```

Soft delete is used instead of physical deletion.

Benefits:

* auditability
* recovery support
* historical data preservation

---

## 5. Increase Product Quantity

### Endpoint

```http
PATCH /api/products/{productId}/variants/{variantId}/increase-quantity
```

### Request Body

```json
{
  "quantity": 10
}
```

---

# 16. Pagination Design

The project uses a reusable generic paged result model.

## PagedResult

Features:

* total items
* total pages
* next/previous page support
* configurable page size
* upper page size protection

Benefits:

* reusable
* scalable
* frontend-friendly

---

# 17. Validation Strategy

FluentValidation is used.

Validation examples:

* required fields
* maximum length
* positive price
* positive quantity

Example:

```csharp
RuleFor(x => x.Price)
    .GreaterThan(0);
```

---

# 18. Error Handling

Global exception middleware is implemented.

Handled cases:

* validation exceptions
* domain exceptions
* unexpected server exceptions

Benefits:

* centralized error handling
* consistent API responses
* cleaner endpoint logic

---

# 19. Performance Optimization

## Implemented Optimizations

### AsNoTracking

Used for read-only queries.

Benefits:

* lower memory usage
* faster query execution

---

### Pagination

Efficient server-side pagination:

```csharp
Skip(...).Take(...)
```

Benefits:

* avoids loading entire dataset
* reduces memory consumption
* improves response time

---

### Database Indexes

Indexes:

* Product.Slug
* ProductVariant.SKU

Benefits:

* faster lookup
* better filtering performance

---

### CancellationToken

CancellationToken is propagated from HTTP request to EF Core.

Benefits:

* prevents unnecessary database execution
* improves scalability
* releases resources earlier

---

# 20. Strong Consistency

The project ensures strong consistency through:

* PostgreSQL ACID transactions
* Unit Of Work
* optimistic concurrency
* aggregate root rules
* transactional SaveChanges

---

# 21. Edge Cases Covered

## Validation Edge Cases

* empty name
* invalid price
* invalid quantity
* duplicate SKU
* duplicate slug

---

## Data Integrity Edge Cases

* negative stock prevention
* deleted products filtering
* invalid variant handling
* missing product handling

---

## API Edge Cases

* invalid pagination values
* page size overflow
* invalid IDs
* concurrency conflicts

---

# 22. Security Considerations

Current implementation:

* input validation
* DTO separation
* controlled entity mutation

Recommended future improvements:

* JWT authentication
* authorization policies
* rate limiting
* API versioning

---

# 23. Environment Variables

Example:

```env
ConnectionStrings__DefaultConnection=Host=localhost;Port=5432;Database=product_db;Username=postgres;Password=postgres
```

---

# 24. Running The Project

## Install Dependencies

```bash
dotnet restore
```

---

## Run Migration

```bash
dotnet ef database update
```

---

## Run Application

```bash
dotnet run
```

---

# 25. Swagger

Swagger is enabled.

Swagger URL:

```txt
/swagger
```

---

# 26. Postman Collection

The project includes:

* Create Product request
* Get Products request
* Update Product request
* Delete Product request
* Increase Quantity request

---

# 27. Limitations

Current limitations:

* no authentication
* no authorization
* no Redis caching
* no distributed transactions
* no event-driven integration
* no audit logging

---

# 28. Future Improvements

Recommended future enhancements:

## Infrastructure

* Redis caching
* Docker support
* CI/CD pipeline
* OpenTelemetry
* Serilog

---

## Architecture

* full CQRS
* domain events
* outbox pattern
* background jobs
* microservices migration

---

## Security

* JWT authentication
* role-based authorization
* API rate limiting

---

## Scalability

* read replicas
* Dapper for read side
* distributed caching
* message queue integration

---

# 29. Conclusion

This backend system was designed with scalability, maintainability, and consistency in mind.

The implementation uses modern .NET backend best practices:

* Clean Architecture
* DDD
* FluentValidation
* Repository Pattern
* Unit Of Work
* PostgreSQL concurrency handling
* Minimal APIs

The system is modular and can be extended easily for enterprise-level e-commerce requirements.
