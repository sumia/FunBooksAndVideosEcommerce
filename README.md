# FunBooksAndVideos - E-Commerce Shop

**FunBooksAndVideos** is an e-commerce application that allows customers to browse books, watch online videos, and subscribe to membership plans for books, videos, or both (premium).

---

## Features

- **DDD-Inspired Structure:** A modular design ensuring separation of concerns.
- **Business Rules Engine:**
  - Implements a **common interface (`IOrderRule`)** for defining business rules.
  - Rules follow the **Single Responsibility Principle (SRP)** and are self-contained.
  - Implements IOrderRuleFactory – A contract to return applicable order rules.
    - Uses Dependency Injection – Injects a list of IOrderRule implementations at runtime.
    - Provides Order Rules – The GetRules() method returns all registered rules dynamically.
  - Rules are loaded dynamically via **Reflection & Dependency Injection (DI)** instead of being manually specified. This logic is configured in `API > OrderRuleExtensions`.
- **Asynchronous Programming:** `async/await` is used where applicable for better performance.

---

## Architecture & Design Principles

### **OOP Design – Composition Over Inheritance**

#### **Overview**  
This project follows **Composition over Inheritance** for **Products and Memberships**, ensuring **scalability, flexibility, and maintainability** while adhering to **SOLID principles**.
Composition-Based Design is the best way to structure the database and application for scalability when handling a growing number of products.

## ---

#### **Why Composition Instead of Inheritance?**  

### **More Flexibility**  
- **Inheritance** creates rigid structures; modifying one class impacts all subclasses.  
- **Composition** allows adding/removing features dynamically via attributes.  

### **Avoids Deep Class Hierarchy**  
- **Inheritance:** `Product → Book → Membership → BookClubMembership` (Hard to manage)  
- **Composition:** `Product { List<ProductAttribute> Attributes }` (More flexible)  

### **Better SOLID Compliance**  
- **SRP:** Each class has a single responsibility.  
- **OCP:** Easily extendable without modifying existing code.  
- **LSP:** No risk of breaking existing functionality.  

## ---

### **Composition-Based Product Model**  
All generic properties in products table and product specific details can go in ProductAttributes table (covered in 'Scaling with Product Attributes' section)
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ProductType Type { get; set; } // Physical, Digital, Membership
    public ProductCategory Category { get; set; } // Books, Videos, Memberships
}
```

### SOLID Principles

- **Single Responsibility Principle (SRP):** Each rule has a dedicated class for a specific task.
- **Open-Closed Principle (OCP):** New rules can be added without modifying existing logic.
- **Dependency Injection (DI):** Injects services like `IMembershipService` and `IShippingService` for better testability.

### Design Patterns

- **Factory Pattern:** `OrderRuleFactory` manages dynamic rule creation.
- **Strategy Pattern:** `IOrderRule` and its implementations (`MembershipActivationRule`, `ShippingSlipRule`) allow dynamic rule application based on conditions.
- **Repository Pattern** - Centralized data access logic.
- **Unit of Work Pattern** - Manages transactions efficiently.

### Core Components

- **EF Core In-Memory Database** for development & testing.
- **API Versioning** for backward compatibility.
- **Logging & Swagger Annotations** for better observability.
- **Seeded Database** to populate initial data.

---

## REST APIs

| API              | Description                                                        |
| ---------------- | ------------------------------------------------------------------ |
| **Customer API** | CRUD operations & membership retrieval by `customerId`             |
| **Products API** | CRUD operations for books, videos & memberships                    |
| **Order API**    | Processes orders & applies rules dynamically using the Rule Engine |

---

## Tests
For simplicity, tests around business rules are covered only at this moment.

---

## Extending the System

### To **add a new rule**:

1. Implement the `IOrderRule` interface.
2. Define the `IsApplicable` method to determine when the rule applies.
3. Implement `Apply` with the necessary business logic.
4. Place the rule class in the `Rule` folder under `Application > PurchaseOrder`.


### Scaling with Product Attributes

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ProductType Type { get; set; } // Physical, Digital, Membership
    public ProductCategory Category { get; set; } // Books, Videos, Memberships
    public List<ProductAttribute> Attributes { get; set; } = new();
}
```

```csharp
public class ProductAttribute
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string AttributeName { get; set; }
    public string AttributeValue { get; set; }
}
```
| ProductId | AttributeName  | AttributeValue        |
|-----------|--------------|-----------------------|
| 1         | "Author"      | "Andrew Hunt"        |
| 1         | "Pages"       | "352"                |
| 3         | "Director"    | "Christopher Nolan"  |
| 3         | "Duration"    | "148 minutes"        |




---

## Summary

FunBooksAndVideos provides a well-structured, modular, and scalable architecture, making it easy to extend and maintain. The business rules engine, combined with best practices like SOLID principles, middleware, design patterns, and dependency injection, ensures clean and efficient code.
