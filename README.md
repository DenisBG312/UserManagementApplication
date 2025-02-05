# User Management Application

## 🚀 Features

- **User CRUD Operations**: Create, Read, Update, Delete users effortlessly.
- **RESTful API**: Fully documented API endpoints for easy integration.
- **PostgreSQL Database**: Reliable and scalable data storage.
- **ASP.NET Core MVC**: Dynamic and responsive front-end.
- **ASP.NET Core Web API**: Powerful and secure back-end.

---
## API Index View
![Api index view](https://github.com/DenisBG312/UserManagementApplication/blob/master/UserManagementApplication.Web/Project-Pictures/API_index.png)

## Web Index View
![Api index view](https://github.com/DenisBG312/UserManagementApplication/blob/master/UserManagementApplication.Web/Project-Pictures/Web_index.png)

## 💡 Tech Stack

- **Frontend**: ASP.NET Core MVC
- **Backend**: ASP.NET Core Web API
- **Database**: PostgreSQL

---

## 🚧 Installation

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [PostgreSQL](https://www.postgresql.org/download/)

### Steps

1. **Clone the Repository**

   ```bash
   git clone https://github.com/DenisBG312/UserManagementApplication.git
   cd UserManagementApplication
   ```

2. **Configure Database**

   - Update the `appsettings.json` file with your PostgreSQL connection string:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=UserManagementDB;Username=your-username;Password=your-password"
   }
   ```

3. **Apply Migrations**

   ```bash
   dotnet ef database update
   ```

4. **Run the Application**

   ```bash
   dotnet run
   ```
   
5. **In order for the application to run correctly, make sure you have run both the API project and the Web project**

---

## 🌐 API Endpoints

### Base URL

```
https://localhost:7183/api/User
```

### Endpoints

- **Get All Users**\
  `GET /api/User/GetAllUsers`

- **Get Specific User by ID**\
  `GET /api/User/GetSpecificUser/{id}`

- **Search Users**\
  `GET /api/User/SearchUser`

- **Create User**\
  `POST /api/User/CreateUser`

  ```json
  {
    "firstName": "John",
    "lastName": "Doe",
    "dateOfBirth": "1990-01-01T00:00:00Z",
    "phoneNumber": "123-456-7890",
    "emailAddress": "john.doe@example.com"
  }
  ```

- **Update User**\
  `PUT /api/User/UpdateUser/{id}`

  ```json
  {
    "firstName": "Jane",
    "lastName": "Doe",
    "dateOfBirth": "1991-02-01T00:00:00Z",
    "phoneNumber": "987-654-3210",
    "emailAddress": "jane.doe@example.com"
  }
  ```

- **Delete User**\
  `DELETE /api/User/DeleteUser/{id}` (The application uses soft deletion)

---

## 🛠️ Project Structure

```
UserManagementApplication/
├── UserManagementApplication.API/         # ASP.NET Core Web API project
├── UserManagementApplication.API.Dto/     # Data Transfer Objects for API
├── UserManagementApplication.Data/        # Data access layer
├── UserManagementApplication.Data.Models/ # Data Models
├── UserManagementApplication.Services.Data/ # Business logic services
├── UserManagementApplication.Web/         # ASP.NET Core MVC project
├── UserManagementApplication.Web.Dtos/    # Data Transfer Objects for Web
└── README.md
```
