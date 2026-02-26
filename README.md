# 📧 Lizi Mail API

A REST API in **C# (.NET)** for managing email sending and authentication.

Provides email sending services and JWT-protected endpoints.

---

## 🧠 About

This project is a backend API built with **.NET 7** (example) to send emails and manage API keys and users.

Includes JWT authentication, MySQL database integration, and some example routes.

---

## 🚀 Features

- ✅ JWT Authentication
- ✅ Email Sending
- ✅ Organized Repositories (User, Email, ApiKey)
- ✅ Scalar/OpenAPI for endpoint testing
- ✅ MySQL Connection
- 📄 Example HTTP collection (`lizi-mail-api.http`)

---

## 🛠️ Technologies

The project was built with:

- 🧩 **C#**
- 🔧 **.NET**
- 📦 **Entity Framework Core**
- 🐬 **MySQL**
- 📘 **Swagger / OpenAPI**
- 💡 Custom Middlewares

---

## 🧩 Prerequisites

Before running the API, you need to have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/)

- MySQL (database)
- Code editor (VS Code, Visual Studio, etc.)

---

## 🚚 How to run locally

1. Clone the repository:

```bash
git clone https://github.com/LuisFernando-hub/lizi-mail-api.git

cd lizi-mail-api

"ConnectionStrings": {

"DefaultConnection": "server=localhost;database=lizi_mail_db;user=root;password=1234"

}

dotnet ef database update
dotnet run

http://localhost:5127/scalar

```
