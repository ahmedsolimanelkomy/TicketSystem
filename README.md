# Ticket System

## Overview

This project is a simple Ticket System built with ASP.NET Core API and Entity Framework Core, designed following Clean Architecture principles.
The system manages tickets for users, where each user can have only one ticket. 
It includes functionalities for creating tickets, retrieving all tickets in descending order by ticket number, and fetching ticket details for a specific user by their mobile number.
The project also incorporates JWT authentication for admin users and provides Swagger documentation for API endpoints.

## Key Features

### 🎟 Ticket Creation
- Create tickets using the user's mobile number.
- Save ticket images as JPEG.
- Ensure each user has only one ticket.

### 📜 Ticket Retrieval
- Retrieve all tickets ordered in descending order by ticket number.
- Fetch ticket details by user's mobile number.

### 🔒 Authentication
- Admin users can register and log in using JWT tokens.

### 📑 Swagger Documentation
- Provides comprehensive documentation for all API endpoints.

## Architecture and Technologies

### 🏛 Clean Architecture
- The project follows Clean Architecture principles, ensuring separation of concerns and promoting maintainability.

### 🗃 Repository Pattern
- Utilizes the repository pattern to abstract data access logic and promote a clean separation between the domain and data access layers.

### 🔄 Unit of Work
- Implements the Unit of Work pattern to manage database transactions and ensure consistency.

### 📊 CQRS (Command Query Responsibility Segregation)
- Separates read and write operations using the CQRS pattern, enhancing performance and scalability.

### 🔄 Automapper
- Uses Automapper for object-object mapping, simplifying the mapping between DTOs and domain models.

### 🔌 Dependency Injection
- Employs dependency injection to manage dependencies and promote testability.

### 🔐 Identity
- Implements ASP.NET Core Identity for managing admin user authentication and authorization.

### 📦 Generic Repository
- Uses a generic repository to provide a reusable data access layer for different entities.

### ⚙️ SOLID Principles
- Adheres to SOLID principles to ensure a robust, maintainable, and scalable architecture.

## Project Structure

### 🏗 Presentation Layer (API)
- Contains controllers for handling HTTP requests.
- Includes Swagger documentation for API endpoints.

### 🧩 Application Layer
- Contains command and query handlers for implementing business logic.
- Includes DTOs, interfaces, and service implementations.

### 🔗 Domain Layer (Core)
- Defines domain entities, value objects, and business logic.
- Contains repository interfaces and domain services.

### ⚙️ Infrastructure Layer
- Implements repository interfaces and data access logic using Entity Framework Core.
- Manages database context and migrations.
- Handles external services and configurations.

## Implementation Details

1. **Entities and DTOs**:
   - **User**: Represents the user entity with properties like `Id`, `MobileNumber`, `Name`, and `Email`.
   - **Ticket**: Represents the ticket entity with properties like `Id`, `TicketNumber`, `UserId`, and `TicketImageUrl`.
   - **CreateTicketDTO**: Data transfer object for creating tickets.
   - **UserDTO**: Data transfer object for user details.

2. **Repositories**:
   - **GenericRepository**: Provides generic data access methods for all entities.
   - **UserRepository**: Provides data access methods specific to the User entity.
   - **TicketRepository**: Provides data access methods specific to the Ticket entity.

3. **Unit of Work**:
   - Manages transactions and coordinates the work of multiple repositories.

4. **Services**:
   - **TicketService**: Contains business logic for ticket creation and retrieval.
   - **UserService**: Contains business logic for user-related operations.

5. **Command and Query Handlers**:
   - **CreateTicketCommandHandler**: Handles the logic for creating tickets.
   - **GetAllTicketsQueryHandler**: Handles the logic for retrieving all tickets.
   - **GetUserTicketQueryHandler**: Handles the logic for fetching a specific user's ticket details.

6. **Authentication and Authorization**:
   - Uses ASP.NET Core Identity to manage admin users.
   - Implements JWT token generation and validation for secure access.

## Database

- **SQL Server**: The project uses SQL Server as the database management system. Entity Framework Core is configured for data access and migrations.

## Conclusion

This Ticket System project is built with a focus on maintainability, scalability, and clean architecture.
The project ensures a robust and efficient implementation by leveraging modern design patterns and best practices such as CQRS, repository pattern, unit of work, and SOLID principles.
Including JWT authentication and a well-structured project layout further enhance the system's usability and extensibility.


