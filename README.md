# Habit-Builders

A full-stack habit tracking application designed to help people build healthy habits, particularly for weight loss. This project was originally developed as a course assignment for Fundamentals of Software Construction.

---

## 📋 Table of Contents

- [Architecture Overview](#-architecture-overview)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Database Schema](#-database-schema)
- [API Endpoints](#-api-endpoints)
- [Frontend Routes](#-frontend-routes)
- [Key Features](#-key-features)
- [Known Issues & Technical Debt](#-known-issues--technical-debt)
- [Setup & Development](#-setup--development)
- [Docker Deployment](#-docker-deployment)
- [Refactoring Notes](#-refactoring-notes)

---

## 🏗️ Architecture Overview

This is a **client-server architecture** with:

- **Backend**: ASP.NET Core 8.0 Web API with Entity Framework Core
- **Frontend**: Vue 3 + Vite SPA with Vant UI components
- **Database**: MySQL (accessed via MySQL.EntityFrameworkCore provider)
- **Communication**: RESTful API via HTTP/HTTPS

```
┌─────────────────┐      HTTP/REST       ┌─────────────────┐      SQL       ┌──────────────┐
│  Vue 3 Frontend │  ═══════════════════► │  .NET 8 Backend │  ═══════════►  │  MySQL DB    │
│  (Vite + Vant)  │                       │  (EF Core)      │                │  (rgUser)    │
└─────────────────┘                       └─────────────────┘                └──────────────┘
```

---

## 🛠️ Tech Stack

### Backend
| Technology | Version | Purpose |
|------------|---------|---------|
| .NET SDK | 8.0 | Runtime & Framework |
| ASP.NET Core | 8.0 | Web API framework |
| Entity Framework Core | 7.0.2 | ORM for database access |
| MySQL.EntityFrameworkCore | 7.0.2 | MySQL provider for EF Core |
| Swashbuckle.AspNetCore | 6.4.0 | Swagger/OpenAPI documentation |

### Frontend
| Technology | Version | Purpose |
|------------|---------|---------|
| Vue | 3.4.21 | Frontend framework |
| Vue Router | 4.3.2 | Client-side routing |
| Vite | 5.2.0 | Build tool & dev server |
| Vant | 4.9.0 | Mobile UI component library |
| Axios | 1.6.8 | HTTP client |
| amfe-flexible | 2.2.1 | Mobile viewport adaptation |

---

## 📁 Project Structure

```
Habit-Builders/
├── habitsBuilderBackEnd/           # .NET 8 Web API
│   ├── habitsBuilderBackEnd/
│   │   ├── Controllers/            # API Controllers
│   │   │   ├── UserController.cs   # User registration, login, friends
│   │   │   ├── RecordController.cs # Health scores tracking
│   │   │   ├── PostController.cs   # Community posts, likes, photos
│   │   │   └── CardController.cs   # Habit cards & checklists
│   │   ├── Models/                 # Entity Framework entities
│   │   │   ├── User.cs             # User entity
│   │   │   ├── Record.cs           # Daily health scores
│   │   │   ├── Post.cs             # Community posts
│   │   │   ├── HabitCard.cs        # Habit tracking cards
│   │   │   └── ChecklistItem.cs    # Individual checklist items
│   │   ├── DTO/                    # Data Transfer Objects
│   │   ├── Services/               # Business logic layer
│   │   │   ├── UserService.cs
│   │   │   ├── RecordService.cs
│   │   │   ├── PostService.cs
│   │   │   └── DailyTaskService.cs
│   │   ├── Repositories/           # Data access layer
│   │   │   └── RecordDbContext.cs  # EF Core DbContext
│   │   ├── appsettings.json        # Configuration (DB connection)
│   │   └── Program.cs              # App entry point
│   └── Dockerfile
│
└── habitsBuilderFrontEnd/          # Vue 3 SPA
    ├── src/
    │   ├── pages/                  # Page components
    │   │   ├── accessment/         # Health assessment pages
    │   │   │   ├── main.vue        # Assessment dashboard
    │   │   │   ├── diet.vue        # Diet scoring
    │   │   │   ├── sleep.vue       # Sleep scoring
    │   │   │   ├── sport.vue       # Exercise scoring
    │   │   │   └── history.vue     # Historical data
    │   │   ├── login/              # Authentication
    │   │   ├── habit/              # Habit tracking & AI
    │   │   ├── comunity/           # Social features
    │   │   └── user/               # User profile
    │   ├── components/             # Reusable components
    │   ├── router/index.js         # Route definitions
    │   ├── http/index.js           # Axios HTTP client
    │   └── state/state.js          # Global state management
    └── Dockerfile
```

---

## 🗄️ Database Schema

**Database**: `rgUser` (MySQL)

### Core Entities

```
┌─────────────┐       ┌─────────────┐       ┌─────────────┐
│    User     │◄──────┤  UserFriend │       │   Record    │
├─────────────┤       ├─────────────┤       ├─────────────┤
│ PK UserId   │       │ PK UserId   │       │ PK RecordId │
│   UserName  │       │ PK FriendId │       │ FK UserId   │
│   Password  │       └─────────────┘       │   dateTime  │
│   Friends   │                             │   sleepscore│
│   Posts     │       ┌─────────────┐       │   dietscore │
│   HabitCards│       │   HabitCard │       │   sportscore│
└─────────────┘       ├─────────────┤       │   totalscore│
       │              │ PK Id       │       └─────────────┘
       │              │ FK UserId   │
       │              │   Category  │       ┌─────────────┐
       │              │   Description│      │ChecklistItem│
       │              └─────────────┘       ├─────────────┤
       │                     │              │ PK Id       │
       │                     │              │ FK CardId   │
       │                     ▼              │   Content   │
       │              ┌─────────────┐       │   IsCompleted│
       └─────────────►│    Post     │       └─────────────┘
                      ├─────────────┤
                      │ PK PostId   │       ┌─────────────┐
                      │ FK UserId   │       │  PostLike   │
                      │   Content   │       ├─────────────┤
                      │   PostedAt  │       │ PK PostLikeId│
                      │   Photos    │◄──────┤ FK PostId   │
                      │   Likes     │◄──────┤ FK UserId   │
                      └─────────────┘       └─────────────┘
                             │
                             ▼
                      ┌─────────────┐
                      │  PostPhoto  │
                      ├─────────────┤
                      │ PK PostPhotoId
                      │ FK PostId   │
                      │   Url       │
                      └─────────────┘
```

### Entity Relationships
- **User** 1:N **Record** (One user has many daily records)
- **User** 1:N **HabitCard** (One user has many habit cards)
- **User** 1:N **Post** (One user can create many posts)
- **User** N:M **User** (Friendship - self-referencing many-to-many)
- **HabitCard** 1:N **ChecklistItem** (A card has multiple checklist items)
- **Post** 1:N **PostPhoto** (A post can have multiple photos)
- **Post** 1:N **PostLike** (A post can have multiple likes)

---

## 🔌 API Endpoints

Base URL: `/api`

### UserController (`/api/user`)
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/register` | Register new user |
| POST | `/login` | User login (returns success boolean) |
| GET | `/{id}` | Get user by ID |
| POST | `/{id}/update-password` | Update user password |
| POST | `/{id}/friends/{friendId}` | Add friend |
| DELETE | `/{id}/friends/{friendId}` | Remove friend |

### RecordController (`/api/record`)
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/dailytask` | Create/update daily record |
| GET | `/history` | Get user's historical records |
| POST | `/data` | Get records by date range |
| GET | `/sportdata` | Get exercise data |
| GET | `/dietdata` | Get diet data |

### PostController (`/api/post`)
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/` | Create new post |
| GET | `/{postId}` | Get post by ID |
| POST | `/{postId}/likes` | Like/unlike a post |

### CardController (`/api/card`)
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/{userId}` | Get user's habit cards |
| POST | `/{userId}` | Create new habit card |
| DELETE | `/{userId}` | Delete habit card |
| POST | `/{userId}/{cardId}/checklist` | Add checklist item |
| DELETE | `/{userId}/{cardId}/checklist/{itemId}` | Delete checklist item |

---

## 🌐 Frontend Routes

| Route | Component | Auth Required | Description |
|-------|-----------|---------------|-------------|
| `/` | `main.vue` | ✅ | Assessment dashboard |
| `/login` | `login.vue` | ❌ | User login |
| `/accessment/diet` | `diet.vue` | ✅ | Diet assessment |
| `/accessment/sleep` | `sleep.vue` | ✅ | Sleep assessment |
| `/accessment/sport` | `sport.vue` | ✅ | Exercise assessment |
| `/accessment/history` | `history.vue` | ✅ | View historical data |
| `/comunity` | `comunity.vue` | ✅ | Community/feed |
| `/habit` | `habit.vue` | ✅ | Habit tracking |
| `/habit/ai` | `ai.vue` | ❌ | AI workout planner |
| `/user` | `user.vue` | ✅ | User profile |

---

## ✨ Key Features

1. **User Management**
   - Registration & login (plaintext passwords ⚠️)
   - Friend system (bidirectional friendship)

2. **Health Assessment**
   - Daily scoring for: Sleep, Diet, Exercise
   - Aggregated total score calculation
   - Historical data visualization

3. **Habit Tracking**
   - Custom habit cards with categories
   - Checklist items for each habit
   - Daily task completion tracking

4. **Community**
   - Post creation with photo uploads
   - Like system for posts
   - Social feed

5. **AI Features**
   - AI-designed workout plans (placeholder implementation)
   - Siri shortcut integration for voice check-ins

---

## ⚠️ Known Issues & Technical Debt

### 🔴 Security Issues
1. **Plaintext Passwords**: Passwords stored and compared in plaintext (no hashing)
   - Location: `UserService.ValidateUserAsync()`, `UserService.UpdatePasswordAsync()`
   - Fix: Implement bcrypt/Argon2 password hashing

2. **No JWT/Session Management**: Authentication is basic - only validates credentials on login, no token/session mechanism
   - Location: `UserController.Login()`, frontend `state.isAuthenticated`
   - Fix: Implement JWT tokens or session cookies

3. **SQL Injection Risk**: Connection string in `appsettings.json` with hardcoded credentials
   - Fix: Use environment variables or secrets management

### 🟡 Code Quality Issues
1. **Typo in "Community"**: Route and folder named `comunity` instead of `community`
   - Affects: `router/index.js`, folder structure

2. **Mixed Languages**: Comments and some responses in Chinese, some in English
   - Inconsistent localization

3. **No Input Validation**: Missing validation on DTOs and request models
   - Example: `UserController.RegisterUser()` doesn't validate UserId format

4. **No Error Handling**: Services don't handle exceptions properly
   - Example: Database connection failures not caught

5. **Unused Code**: 
   - `DailyTaskService` exists but unclear usage
   - `todolist` page referenced but minimal implementation

### 🟠 Architecture Issues
1. **No Repository Pattern**: Services directly use `DbContext` (tight coupling)
   - Fix: Implement repository + unit of work pattern

2. **DTO Mismatch**: Some entities lack proper DTOs (e.g., `Record` returned directly)

3. **No Pagination**: API returns all records without pagination
   - Affects: `GetUserPosts`, `GetHistory`

4. **File Upload Security**: Photo uploads saved with original filenames
   - Location: `PostController` file upload logic
   - Risk: Path traversal attacks

### 🔵 Frontend Issues
1. **Hardcoded API URL**: Frontend uses `/api` base URL without environment configuration
   - Location: `http/index.js`

2. **No State Management**: Using simple reactive object instead of Pinia/Vuex
   - Location: `state/state.js`

3. **Missing Error UI**: HTTP errors only logged to console
   - Location: `http/index.js` post/get functions

4. **Type Safety**: No TypeScript usage

---

## 🚀 Setup & Development

### Prerequisites
- .NET 8.0 SDK
- Node.js 20+
- MySQL Server

### Database Setup
```sql
-- Create database
CREATE DATABASE rgUser;

-- Create user (optional)
CREATE USER 'habituser'@'localhost' IDENTIFIED BY 'yourpassword';
GRANT ALL PRIVILEGES ON rgUser.* TO 'habituser'@'localhost';
FLUSH PRIVILEGES;
```

Update connection string in `habitsBuilderBackEnd/habitsBuilderBackEnd/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DB": "Server=localhost;Database=rgUser;User=your_user;Password=your_password"
  }
}
```

### Backend Development
```bash
cd habitsBuilderBackEnd/habitsBuilderBackEnd
dotnet restore
dotnet run
# API will be available at https://localhost:7001 or http://localhost:5000
# Swagger UI at /swagger
```

### Frontend Development
```bash
cd habitsBuilderFrontEnd
npm install
npm run dev
# Development server at http://localhost:5173
```

---

## 🐳 Docker Deployment

### Build & Run Backend
```bash
cd habitsBuilderBackEnd
docker build -t habit-backend .
docker run -p 5000:5000 -e ConnectionStrings__DB="your_connection_string" habit-backend
```

### Build & Run Frontend
```bash
cd habitsBuilderFrontEnd
docker build -t habit-frontend .
docker run -p 5173:5173 habit-frontend
```

### Docker Compose (Recommended)
Create a `docker-compose.yml`:
```yaml
version: '3.8'
services:
  mysql:
    image: mysql:8.0
    environment:
      MYSQL_ROOT_PASSWORD: 123456
      MYSQL_DATABASE: rgUser
    ports:
      - "3306:3306"
  
  backend:
    build: ./habitsBuilderBackEnd
    ports:
      - "5000:5000"
    environment:
      ConnectionStrings__DB: "Server=mysql;Database=rgUser;User=root;Password=123456"
    depends_on:
      - mysql
  
  frontend:
    build: ./habitsBuilderFrontEnd
    ports:
      - "5173:5173"
```

---

## 📝 Refactoring Notes

### Priority 1: Security
- [ ] Implement password hashing (bcrypt)
- [ ] Add JWT authentication
- [ ] Move secrets to environment variables
- [ ] Add input validation (FluentValidation)
- [ ] Sanitize file uploads

### Priority 2: Code Quality
- [ ] Add global exception handling middleware
- [ ] Implement proper logging (Serilog)
- [ ] Add async/await consistency check
- [ ] Fix typos (`comunity` → `community`)

### Priority 3: Architecture
- [ ] Implement Repository pattern
- [ ] Add pagination to list endpoints
- [ ] Create proper DTOs for all entities
- [ ] Add API versioning
- [ ] Implement CQRS for complex operations

### Priority 4: Frontend
- [ ] Add TypeScript
- [ ] Migrate to Pinia for state management
- [ ] Add error boundaries
- [ ] Implement proper loading states
- [ ] Add form validation (VeeValidate)

### Priority 5: DevOps
- [ ] Add unit tests (xUnit for backend, Vitest for frontend)
- [ ] Add integration tests
- [ ] Set up CI/CD pipeline
- [ ] Add health checks endpoint

---

## 📄 License

This project was created for educational purposes as a course assignment.

---

## 🙏 Credits

Originally developed as a team project for Fundamentals of Software Construction course.
