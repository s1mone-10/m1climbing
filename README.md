# m1climbing

A platform for climbers to discover crags and track completed routes.  
*Test project for practicing ASP.NET Core backend development.*

## 🛠 Tech Stack
- ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core  
- SQL Server (LocalDB)  

## 📦 Features
- ✅ User authentication with ASP.NET Identity
- 🔐 Authorization via policy-based access control
- 🧗 CRUD operations for Crags, Sectors, and Routes  
- 📌 Track completed routes  
- 🌐 RESTful API

## 📡 API Endpoints

### Public (no authentication required)
- `GET /api/Climbing/crags`
- `GET /api/Climbing/crags/{id}`
- `GET /api/Climbing/crags/{cragId}/sectors`
- `GET /api/Climbing/crags/{cragId}/routes`

### Authenticated (requires ManageClimbingData policy)
- `POST /api/Climbing/crags`
- `PUT /api/Climbing/crags/{id}`
- `DELETE /api/Climbing/crags/{id}`
