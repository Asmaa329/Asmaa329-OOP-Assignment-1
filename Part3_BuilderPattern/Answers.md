# Part 3: Builder Pattern — Written Answers

## Task 3.1 Questions

### 1. Why is a single 20-parameter constructor a problem in practice?
* **Call-site Readability:** Instantiating an object like `new Invoice("INV-1001", "Asmaa", "Main St", "Ashmoun", "Menofia", "32911", ...)` makes the code extremely difficult to read and maintain. Without checking the constructor signature, developers cannot identify which positional value corresponds to which property.
* **Transposition Risk (Type Inversion):** When multiple adjacent parameters share the same primitive data type (e.g., passing two `string` parameters like `BillingCity` vs. `ShippingCity`, or two `decimal` values like `SubTotal` vs. `TaxAmount`), passing arguments in the wrong order will not trigger a compiler error. This leads to subtle runtime logic bugs that are hard to trace.
* **Extensibility and Maintenance Bottlenecks:** Adding a single new optional property in the future forces developers to either modify every instantiation call site across the entire codebase or write a tedious chain of constructor overloads (Telescoping Constructor Anti-pattern).

### 2. Is this purely a "constructor is too long" problem, or is there a deeper design issue?
It is a **deeper architectural design issue**. Having roughly 20 loosely related properties directly on the `Invoice` class violates the **Single Responsibility Principle (SRP)** and suffers from **Primitive Obsession / God Object anti-pattern**:
* The `Invoice` class should only be concerned with high-level invoice management.
* It offloads and delegates specific concerns (like street, city, zip code validation) instead of mixing billing addresses, shipping addresses, and payment transactions into a single flat structure. Address details belong to an encapsulated `Address` Value Object/Class.

---

## Task 3.3 Comparison Questions

### Why is the Composed Builder approach better than a single big builder?

* **Single Responsibility Principle (SRP):** Each builder owns a single, focused domain:
  * `AddressBuilder` strictly manages building and validating structural address data.
  * `OrderBuilder` strictly manages order calculations and payment details.
  * `InvoiceBuilder` orchestrates the top-level invoice creation without micromanaging address or payment fields.
* **Independent Validation:** `AddressBuilder` enforces domain validation rules (e.g., checking for non-empty street/city and valid zip codes) at the address level. The parent `Invoice` class does not need any internal knowledge of street/zip code rules.
* **Code Reuse:** The exact same `AddressBuilder` instance logic is reused for constructing both the `BillingAddress` and `ShippingAddress`. Without composition, all address building methods and validation logic would have to be duplicated across the single large builder.
* **Call-site Readability:** The composed fluent interface produces expressive, self-documenting code that mimics natural domain language, grouping related properties visually into cohesive blocks.