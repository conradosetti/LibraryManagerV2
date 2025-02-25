# Library Management API

## Overview
The Library Managemer API allows users to manage books, users, and loan records. It follows a structured relationship where:

- Each **User** can borrow multiple books.
- Each **Book** can only be borrowed by one user at a time.
- A **Loan** represents the relationship between a User and a Book. A book must be returned before being borrowed again.

## Entities
### User
Represents a library user who can borrow books.

### Book
Represents a book in the library that can be borrowed.

### Loan
Tracks the borrowing of a book by a user and ensures books can only be loaned once at a time.

## API Endpoints

### **Book Routes**
- `GET /api/books` - Retrieves all books.
- `GET /api/books/{id}` - Retrieves a specific book by ID.
- `POST /api/books` - Creates a new book.
- `DELETE /api/books/{id}` - Deletes a book by ID.
- `PUT /api/books/{id}/return-loan` - Marks a book as returned.

### **User Routes**
- `GET /api/users/{id}` - Retrieves a user by ID.
- `POST /api/users` - Creates a new user.

### **Loan Routes**
- `GET /api/loans` - Retrieves all loan records.
- `GET /api/loans/{id}` - Retrieves a specific loan record.
- `POST /api/loans` - Creates a new loan record.

---

## How to Set Up the Project Locally
### Prerequisites
Make sure you have the following installed:
- .NET 7.0 or later
- SQL Server or SQLite (depending on configuration)
- Entity Framework Core

### Installation Steps

1. **Clone the repository:**
   ```sh
   git clone https://github.com/conradosetti/LibraryManagerV2.git
   ```

2. **Set up the database:**
   - Update `appsettings.json` with the correct database connection string.
   - Run database migrations:
     ```sh
     cd LibraryManager.Infrastrucure
     dotnet ef migrations add PrimeiraMigration -o Persistence/Migrations -s ..\LibraryManager.API\
     dotnet ef database update -s ..\LibraryManager.API\
     ```

3. **Build the project:**
   ```sh
   dotnet build
   ```

4. **Run the API:**
   ```sh
   dotnet run
   ```

5. **Access the API documentation (if using Swagger):**
   Open `http://localhost:5260/swagger` in your browser.

---

## Technologies Used
- **ASP.NET Core** for API development
- **Entity Framework Core** for database management
- **SQL Server/SQLite** for data storage
- **Swagger** for API documentation

---

## Contributing
Feel free to submit issues or pull requests if you want to contribute to the project.

---

