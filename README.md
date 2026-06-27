# 🧠 Forma — AI-Assisted Fitness Backend API

Forma is an ASP.NET Core Web API backend for a fitness tracking mobile app (Flutter frontend), built with Clean Architecture. It manages workouts, nutrition, hydration, and user profiles, with planned AI-driven coaching features that reason over a user's training and nutrition history.

---

## 🏗️ Architecture

The solution follows **Clean Architecture**, split into four projects:

```
Forma.Api             → Controllers, Middleware, Program.cs (Presentation Layer)
Forma.Application     → Services, DTOs, Interfaces, Business Logic
Forma.Domain           → Entities, Enums, Core Business Rules
Forma.Infrastructure  → EF Core, Repositories, Security, Email, File Storage
```

Each layer only depends on the layers inside it (`Api` → `Application` → `Domain`), keeping business logic decoupled from frameworks, databases, and external services.

---

## ⚙️ Tech Stack

- **Framework:** ASP.NET Core (.NET 10)
- **Database:** PostgreSQL + Entity Framework Core
- **Authentication:** JWT Bearer tokens
- **Password Hashing:** BCrypt
- **Email:** MailKit (Gmail SMTP) for OTP delivery
- **File Storage:** Local static file storage (`wwwroot`), abstracted behind an interface for future cloud storage support
- **Caching:** `IMemoryCache` for transient OTP storage

---

## ✅ Current Features

### Authentication & Email Verification
- OTP-based email verification flow (Pattern A — verify before account creation):
  1. `POST /api/auth/send-otp` — sends a 6-digit OTP to the provided email
  2. `POST /api/auth/verify-otp` — verifies the OTP (single-use, 10-minute expiry)
  3. `POST /api/auth/register` — creates the account only if the email was verified
  4. `POST /api/auth/login` — authenticates and returns a JWT
- Passwords hashed with **BCrypt**, never stored or returned in plain text
- JWT generation with configurable issuer, audience, secret, and expiry
- Global exception handling middleware maps custom exceptions (`ConflictException`, `UnauthorizedException`, `NotFoundException`, `ArgumentException`) to proper HTTP status codes

### User Profile
- `POST /api/profile/upload-picture` *(authenticated)* — uploads a profile picture, stores it on disk, and saves the relative URL on the `User` record
- Returns the picture's full absolute URL in the response
- File validation: allowed types (`.jpg`, `.jpeg`, `.png`, `.webp`), 5MB max size
- Re-uploading overwrites the previous picture (filename keyed by `UserId`)

### Domain Model
- **User** — profile data, training goal (`Goal` enum), gender, physical stats
- **Workout / WorkoutExercise / Set** — full workout logging structure with set types (Normal, WarmUp, DropSet, SuperSet, FailureSet) and RPE tracking
- **Exercise / ExerciseMuscleInvolvement** — exercises mapped to muscles with percentage contribution, designed for future AI-driven muscle activation analysis
- **Meal / FoodItem / MealFoodItem** — nutrition logging with macro tracking
- **WaterIntake** — hydration logging
- **HydrationService** — calculates a user's daily water target based on body weight and workout duration

---

## 🔐 Security Notes

- JWT secret and email credentials are kept out of source control via **.NET User Secrets** (local dev) — never committed in `appsettings.json`
- Login error messages are intentionally generic ("Invalid email or password") to avoid leaking which accounts exist
- OTPs are single-use and stored only in memory (no DB persistence), with a short-lived "verified" flag bridging OTP verification and final registration

---

## 🚧 In Progress / Planned

- [ ] `WorkoutService` + `WorkoutController` (CRUD for workouts, exercises, sets)
- [ ] `NutritionService` + `MealController`
- [ ] `HydrationController` (expose existing `HydrationService` via API)
- [ ] FluentValidation for request validation
- [ ] Refresh token support
- [ ] `IAIService` — AI-generated coaching advice based on user training/nutrition data
- [ ] Docker + CI/CD pipeline
- [ ] Swagger/OpenAPI documentation

---

## 🗂️ Project Structure

```
Forma/
├── Forma.Api/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   └── ProfileController.cs
│   ├── Middleware/
│   │   └── ExceptionHandlingMiddleware.cs
│   └── Program.cs
│
├── Forma.Application/
│   ├── DTOs/Auth/
│   ├── Exceptions/
│   ├── Interfaces/
│   ├── Services/
│   └── Settings/
│
├── Forma.Domain/
│   ├── Common/ (BaseEntity)
│   ├── Entities/
│   └── Enums/
│
└── Forma.Infrastructure/
    ├── Email/
    ├── Persistence/ (DbContext, Migrations)
    ├── Repositories/
    ├── Security/ (BCryptPasswordHasher, JwtTokenGenerator, OtpService)
    └── Storage/ (LocalFileStorageService)
```

---

## 🧪 Testing the API

A typical end-to-end flow using Postman or similar:

```http
POST /api/auth/send-otp
{ "email": "user@example.com" }

POST /api/auth/verify-otp
{ "email": "user@example.com", "otp": "123456" }

POST /api/auth/register
{ "fullName": "...", "email": "user@example.com", "password": "...", ... }

POST /api/auth/login
{ "email": "user@example.com", "password": "..." }

POST /api/profile/upload-picture   (Authorization: Bearer <token>, form-data: file)
```

---

## 👤 Author

**Ahmed Abdulrahman Amin**
Backend Developer | ASP.NET Core Enthusiast
[LinkedIn](https://www.linkedin.com/in/ahmed-abdulrahman-amin)