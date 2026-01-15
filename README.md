# RailwayTicketsAPI Documentation

A modern, high-performance RESTful API built with **.NET 9** for managing railway ticket bookings, schedules, and user management.

## 🚀 Overview

**RailwayTicketsAPI** provides a robust backend solution for railway systems. It handles the core logic for searching train routes, managing seat inventory, processing ticket purchases, and maintaining user profiles.

### Technical Stack

* **Framework:** .NET 9 Web API
* **Language:** C# 13
* **Database:** SQL Server (via Entity Framework Core)
* **Authentication:** JWT (JSON Web Tokens)
* **Documentation:** Swagger / OpenAPI 3.0

---

## 📂 Project Structure

The project follows a clean architecture/layered approach to ensure maintainability:

```text
RailwayTicketsAPI/
├── Controllers/         # API Endpoints (Tickets, Trains, Users, etc.)
├── Data/                # DbContext and Migrations
├── Models/              # Database Entities (Train, Station, Ticket, User)
├── DTOs/                # Data Transfer Objects for requests/responses
├── Services/            # Business Logic Layer
├── Repository/          # Data Access Layer
├── Middleware/          # Custom Exception Handling / Logging
└── Program.cs           # Application configuration & Dependency Injection

```

---

## 🛠️ Getting Started

### Prerequisites

* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or LocalDB)
* IDE: Visual Studio 2022 (v17.12+) or VS Code

### Installation

1. **Clone the repository:**
```bash
git clone https://github.com/nikabara/RailwayTicketsAPI.git
cd RailwayTicketsAPI

```


2. **Update Database Connection:**
Modify the `ConnectionStrings` in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=RailwayDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

```


3. **Apply Migrations:**
```bash
dotnet ef database update

```


4. **Run the Application:**
```bash
dotnet run

```


The API will be available at `https://localhost:5001` (or the port specified in your `launchSettings.json`).

---

## 🔌 API Endpoints

### 🎫 Tickets

| Method | Endpoint | Description |
| --- | --- | --- |
| `GET` | `/api/tickets` | Retrieve all tickets for the authenticated user. |
| `POST` | `/api/tickets/book` | Book a new ticket. |
| `DELETE` | `/api/tickets/{id}` | Cancel a ticket booking. |

### 🚂 Trains & Schedules

| Method | Endpoint | Description |
| --- | --- | --- |
| `GET` | `/api/trains` | Search for available trains between stations. |
| `GET` | `/api/trains/{id}` | Get detailed information about a specific train. |

### 👤 Authentication

| Method | Endpoint | Description |
| --- | --- | --- |
| `POST` | `/api/auth/register` | Create a new user account. |
| `POST` | `/api/auth/login` | Authenticate and receive a JWT token. |

---

## 🔐 Security

The API uses **JWT Authentication**. To access protected endpoints:

1. Login via `/api/auth/login` to receive a token.
2. Include the token in the `Authorization` header for subsequent requests:
`Authorization: Bearer <your_token_here>`

---

## 🧪 Testing

To run the automated tests:

```bash
dotnet test

```

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](https://www.google.com/search?q=LICENSE) file for details.

## 👥 Contributors

* **Nika Barabadze** ([@nikabara](https://github.com/nikabara)) - Lead Developer
