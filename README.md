# Capstone - Insurance Policy Management System

A RESTful API for managing insurance policies, user enrollments, and authentication built with ASP.NET Core and PostgreSQL.

## 🏗️ Architecture

This project follows a **layered architecture** pattern:

```
┌─────────────────────────────────────┐
│         Controllers Layer           │  ← HTTP Request Handling
├─────────────────────────────────────┤
│          Services Layer             │  ← Business Logic
├─────────────────────────────────────┤
│        Repositories Layer           │  ← Data Access
├─────────────────────────────────────┤
│      Data Layer (EF Core)           │  ← ORM & Database Context
└─────────────────────────────────────┘
```

### Project Structure

```
Capstone/
├── Controller/          # API endpoints
│   ├── AuthController.cs
│   ├── PoliciesController.cs
│   ├── EnrollmentController.cs
│   ├── AdminEnrollment.cs
│   └── UserController.cs
├── Services/            # Business logic layer
│   ├── IAuthService.cs / AuthService.cs
│   ├── IPolicyService.cs / PolicyService.cs
│   ├── IEnrollmentService.cs / EnrollmentService.cs
│   └── IUserService.cs / UserService.cs
├── Repositories/        # Data access layer
│   ├── IUserRepository.cs / UserRepository.cs
│   ├── IpolicyRepository.cs / PolicyRepository.cs
│   └── IEnrollmentRepository.cs / EnrollmentRepository.cs
├── Entities/            # Domain models
│   ├── User.cs
│   ├── Policy.cs
│   └── Enrollments.cs
├── DTOs/                # Data transfer objects
│   ├── LoginDto.cs
│   ├── RegisterDto.cs
│   ├── CreatePolicyDto.cs
│   └── EnrollmentDto.cs
├── Filters/             # Middleware & filters
│   ├── GlobalExceptionFilter.cs
│   ├── GlobalResponseFilter.cs
│   └── ResponseTimeFilter.cs
├── Data/
│   └── AppDbContext.cs  # EF Core database context
└── Scripts/             # SQL migration scripts
    ├── User.sql
    ├── Policy.sql
    └── Enrollment.sql
```

## 🚀 Tech Stack

- **Framework:** ASP.NET Core 10.0
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core (Npgsql)
- **Authentication:** JWT Bearer Tokens
- **Password Hashing:** BCrypt.Net
- **API Documentation:** Swagger/OpenAPI

## 📦 Dependencies

```xml
- BCrypt.Net-Next (v4.0.3)
- Microsoft.AspNetCore.Authentication.JwtBearer (v10.0.2)
- Npgsql.EntityFrameworkCore.PostgreSQL (v10.0.0)
- Swashbuckle.AspNetCore (v10.1.2)
- System.IdentityModel.Tokens.Jwt (v8.15.0)
```

## 🔑 Core Features

### Authentication & Authorization
- JWT-based authentication
- Role-based authorization (User/Admin)
- Secure password hashing with BCrypt

### Policy Management
- CRUD operations for insurance policies
- Policy activation/deactivation
- Premium amount tracking

### Enrollment System
- Users can enroll in policies
- Admin approval workflow
- Status tracking (Pending/Approved/Rejected)

### Global Filters
- Exception handling
- Response formatting
- Response time logging

## 🗄️ Database Schema

### Users Table
```sql
- id (int, PK)
- name (string)
- email (string, unique)
- password_hash (string)
- role (string)
- created_at (datetime)
- updated_at (datetime)
```

### Policy Table
```sql
- id (int, PK)
- policy_name (string, 3-100 chars)
- policy_description (string, 10-500 chars)
- premium_amount (int, 1-1000000)
- is_active (boolean)
- created_at (datetime)
```

### Enrollments Table
```sql
- id (int, PK)
- user_id (int, FK)
- policy_id (int, FK)
- status (string: Pending/Approved/Rejected)
- requested_at (datetime)
- approved_at (datetime, nullable)
```

## ⚙️ Configuration

### `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=capstone;Username=postgres;Password=yourpassword"
  },
  "Jwt": {
    "Secret": "your-secret-key",
    "Issuer": "your-issuer",
    "Audience": "your-audience"
  }
}
```

### Environment Variables
- `JWT_SECRET` - Override JWT secret from environment

## 🏃 Getting Started

### Prerequisites
- .NET 10.0 SDK
- PostgreSQL 12+
- IDE (Visual Studio, VS Code, or Rider)

### Installation

1. Clone the repository
```bash
git clone <repository-url>
cd capstone
```

2. Restore dependencies
```bash
dotnet restore
```

3. Update database connection string in `appsettings.json`

4. Run database scripts
```bash
# Execute scripts in Scripts/ folder in order:
# 1. User.sql
# 2. Policy.sql
# 3. Enrollment.sql
```

5. Run the application
```bash
dotnet run --project Capstone/Capstone.csproj
```

6. Access Swagger UI
```
https://localhost:<port>/swagger
```

## 🐛 Technical Debt & Known Issues

### 🔴 High Priority

1. **Naming Inconsistencies**
   - `IuserService` should be `IUserService` (capitalization)
   - `UserCOntroller.cs` has typo (should be `UserController.cs`)
   - `PolicyServie.cs` has typo (should be `PolicyService.cs`)

2. **Missing Database Relationships**
   - No navigation properties between User/Policy/Enrollment entities
   - Foreign key constraints may not be properly configured in EF Core
   - Consider adding: `public User? User { get; set; }` in Enrollment

3. **Security Concerns**
   - JWT secret should NEVER be in `appsettings.json` (use User Secrets or env vars)
   - No password complexity validation
   - Missing rate limiting on authentication endpoints
   - No refresh token mechanism

4. **Error Handling**
   - GlobalExceptionFilter needs proper logging integration
   - API errors may expose sensitive stack traces

### 🟡 Medium Priority

5. **Missing Validation**
   - No email uniqueness validation in service layer
   - Enrollment business rules not enforced (e.g., can user enroll in same policy twice?)
   - No validation for duplicate policy names

6. **Repository Pattern Issues**
   - Methods might be returning EF entities directly instead of DTOs
   - No AsNoTracking() for read-only queries (performance issue)

7. **Missing Features**
   - No pagination on list endpoints
   - No filtering/sorting capabilities
   - No soft delete implementation
   - Missing audit logging

8. **Testing**
   - No unit tests
   - No integration tests
   - No test coverage

### 🟢 Low Priority

9. **Code Quality**
   - Magic strings for roles ("Admin", "User") - should be constants
   - Repeated validation logic across DTOs
   - Missing XML documentation comments

10. **Performance**
    - No caching strategy
    - No database indexing strategy documented
    - Response time filter logs but doesn't expose metrics

11. **DevOps**
    - `/bin` and `/obj` were committed to Git (now in .gitignore)
    - No CI/CD pipeline
    - No Docker containerization
    - No health check endpoints

