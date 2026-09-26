# Procedural C++ Code Critique

## 1. Global State Management & Shared Data Risks
* **Problem:** All domain data is stored in global arrays (`customerIds`, `productPrices`, `orderIsPaid`, etc.) along with global counters (`customerCount`, `productCount`, `orderCount`).
* **Why it is a problem:** Every function in the file has full read/write access to these global variables without any encapsulation or access control.
* **Potential Risks:**
  * Accidental data mutation by unrelated functions.
  * Extensibility and maintainability issues when scaling or introducing concurrent operations.
  * Extreme difficulty in tracking down bugs and state corruption during execution.

## 2. Parallel Arrays Instead of Domain Entities
* **Problem:** Entities like `Customer`, `Product`, and `Order` do not exist as unified data structures or objects. Instead, their attributes are scattered across separate parallel arrays linked only by array indexing (e.g., `customerNames[i]`, `customerEmails[i]`).
* **Why it is a problem:** Keeping parallel arrays in sync is fragile and error-prone.
* **Potential Risks:**
  * Inserting, deleting, or reordering items in one array without updating all related arrays breaks data integrity completely.
  * Memory is wasted by pre-allocating fixed-size arrays (`MAX_CUSTOMERS = 50`, `MAX_ORDERS = 100`) regardless of actual usage.

## 3. Fixed Memory Allocation Limits
* **Problem:** The application uses fixed-size primitive C-style arrays bounded by constants (`MAX_CUSTOMERS`, `MAX_PRODUCTS`, `MAX_ORDERS`, `MAX_LINES_PER_ORDER`).
* **Why it is a problem:** Hardcoded upper limits severely restrict application scalability.
* **Potential Risks:**
  * The system rejects valid new data once the array capacity is reached.
  * Arbitrary constraints like `MAX_LINES_PER_ORDER = 20` enforce artificial business limits.

## 4. Tight Coupling of Business Logic with Console I/O
* **Problem:** Core business logic (such as checking stock, updating inventory, and calculating order totals with VIP discounts) is directly mixed with `std::cout` error prints and console input operations (`runInteractiveMenu`).
* **Why it is a problem:** Violates the Single Responsibility Principle (SRP).
* **Potential Risks:**
  * Business calculations cannot be reused in other application interfaces (e.g., a Web API, Desktop GUI, or Unit Tests).
  * Any change to user presentation forces modifications inside core transaction rules.

