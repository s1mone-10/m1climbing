# m1project

Test project to learn ASP.NET Core backend development.

🧗 Overview
This is a simple web application for browsing a database of climbing crags. All users can explore crags, sectors, and routes. Registered users can also track the routes they’ve completed.

🛠 Tech Stack
ASP.NET Core MVC

Entity Framework Core

ASP.NET Core Identity

SQL Server (LocalDB)

📦 Features
Authentication with Identity

CRUD for Crags, Sectors, Routes

Track completed routes

REST API for external access to crag/sector/route data

📡 API
Basic read-only endpoints (e.g., JSON output):

GET /api/crags — list all crags

GET /api/crags/{id} — crag details

GET /api/sectors/{id} — sector details

GET /api/routes/{id} — route details

All API routes return data in JSON format. No authentication required (for now).

⚙️ Getting Started
Clone the repo
git clone https://github.com/yourusername/m1climbing.git

Open in Visual Studio

Run migrations:

pgsql
Copy
Edit
Update-Database
Run the app

📚 Purpose
This is a learning project to explore:

MVC pattern and Razor views

Code-first database design

REST API creation in ASP.NET Core

Identity-based authentication

📄 License
MIT — free to use and adapt.
