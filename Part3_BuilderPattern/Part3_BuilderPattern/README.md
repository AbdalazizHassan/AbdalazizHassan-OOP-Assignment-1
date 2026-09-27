# Part 3: Builder Pattern & Composed Builders

## Task 3.1 Questions

### 1. Why is a single 20-parameter constructor a problem in practice?
* **Call-site Readability**: Passing 20 arguments directly into a constructor makes the code hard to read and maintain, as it's difficult to identify which value corresponds to which property without inspecting the constructor signature
* **Type Safety & Parameter Ordering Risk**: When multiple parameters share the same data type it is extremely easy to pass values in the wrong order. The compiler won't catch these subtle logical errors
* **Flexibility & Optional Values**: Constructors force callers to provide values for every single parameter (or pass `null`/default values). Adding new optional parameters requires adding more overloaded constructors or modifying existing calls across the codebase

### 2. Is this purely a "constructor is too long" problem?
No, it is a deeper design issue. Putting ~20 loosely related properties into a single class or constructor violates the **Single Responsibility Principle (SRP)**. Address information, customer data, and financial calculations should be domain-separated into cohesive Value Objects or components rather than being flattened into one monolithic structure

---

## Task 3.3 Question

### Why is this composed version better than a single big builder?
* **Single Responsibility**: Each builder manages its own logical domain (`AddressBuilder` owns address components; `InvoiceBuilder` owns the overarching order and customer identity).
* **Independent Validation**: `AddressBuilder` enforces address-specific rules (e.g., requiring street and city) independently without coupling that logic to the invoice initialization.