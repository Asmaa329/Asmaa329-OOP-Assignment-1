# Architectural Critique of the C++ Procedural Order System

## Overview
The C++ codebase implements a basic Order Management System using a procedural paradigm. While functional for small scale execution, it contains several critical architectural flaws regarding data integrity, maintainability, scalability, and code structure.

---

## 1. Parallel Arrays Instead of Object Encapsulation

### Problem
The system uses multiple separate global arrays to store attributes of entities (e.g., `customerIds`, `customerNames`, `customerEmails`, `customerCities`, `customerIsVip` for `Customer`).

### Consequence
- **Loss of Data Cohesion:** The attributes of a single logical entity are scattered across multiple arrays, tied together only by array indices (`i`).
- **Data Corruption Risks:** If one array is modified, inserted into, or deleted from without synchronously updating all related arrays, the indices become misaligned. A customer’s ID would point to another customer's name or email.
- **Scalability Limitation:** Adding a new property (e.g., `Customer Address`) requires creating yet another parallel array and updating every single function that manipulates customer data.

---

## 2. Unchecked Buffer Sizes & Fixed Capacity Limits

### Problem
Entities are stored in fixed-size arrays with arbitrary limits (e.g., maximum 50 customers, 100 products, 100 orders, 20 lines per order).

### Consequence
- **Buffer Overflow & Crashes:** Adding items beyond the hardcoded maximum limits will either cause runtime buffer overflows or silent rejections if strict bounds checks are not enforced across every entry point.
- **Wasted Memory:** Pre-allocating fixed-size arrays reserves memory upfront regardless of whether the capacity is actually used, which is highly inefficient.

---

## 3. Two-Dimensional Arrays for Relationships (`OrderLines`)

### Problem
Order items are stored using a fixed two-dimensional array `lineProductIndexes[100][20]` alongside `lineQuantities[100][20]`, mapping order indices to product indices and quantities.

### Consequence
- **Rigid Schema:** An order can never have more than 20 items due to the hardcoded row length.
- **Tight Coupling to Array Indices:** Storing raw array indices (`productIndex`) instead of stable identifiers or direct entity references means that reordering or deleting products inside the `products` array invalidates all existing order lines.

---

## 4. Global State & Procedural Coupling

### Problem
All data storage vectors/arrays and application states are stored as global variables, accessible and mutable by any function in the program.

### Consequence
- **Lack of Encapsulation & Security:** Any function can inadvertently mutate global arrays without going through validation logic (e.g., setting `productStock` directly to a negative number).
- **Concurrency & Testability Issues:** The code cannot be unit-tested isolatedly because functions implicitly depend on and alter shared global state. Creating multiple instances of the system (e.g., for multi-tenant environments) is impossible without rewriting the entire program.

---

## 5. Weak Domain Logic Validation

### Problem
Validation checks (e.g., checking if an order is already paid before adding new items, verifying stock availability) are mixed directly into global procedural functions alongside console input/output logic.

### Consequence
- **Bypassed Rules:** If a new feature or interface (like an API or web front-end) interacts with the system, it could easily bypass business constraints unless the procedural functions are strictly reused.
- **Code Duplication:** Business logic and console UI logic are tightly coupled, preventing reusability across different user interfaces or presentation layers.