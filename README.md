# Product Management System

A complete full-stack product management system built with Angular 20 and .NET 9 using Clean Architecture principles.

## 🚀 Features

### **Frontend (Angular 20)**
- **Modern UI**: Built with Angular Material Design components
- **Reactive Forms**: Advanced validation with real-time error feedback
- **Server-side Pagination**: Efficient data loading and navigation
- **CRUD Operations**: Create, read, update, and delete products
- **Confirmation Dialogs**: User-friendly deletion confirmations
- **Responsive Design**: Works on desktop and mobile devices
- **Signals Architecture**: Latest Angular 20 reactive patterns

### **Backend (.NET 9)**
- **RESTful API**: Implements CRUD operations (`GET`, `POST`, `PUT`, `DELETE`) following REST standards
- **Clean Architecture**: Layered project structure (`Domain`, `Application`, `Infrastructure`, `API`) for better separation of concerns
- **Data Validation**: Uses `FluentValidation` to ensure input data integrity
- **Interactive Documentation**: Integrated `Swagger/OpenAPI` for exploring and testing API endpoints
- **Global Error Handling**: Custom middleware for centralized exception handling
- **Entity Framework Core**: Database first approach with SQL Server

## 🛠 Requirements

### **Backend**
- .NET 9 SDK
- SQL Server (local instance or cloud database)
- Visual Studio Code or Visual Studio 2022

### **Frontend**
- Node.js (v20.19.0 or higher)
- npm or yarn
- Angular CLI (v20)

### **General**
- Git

## ⚙️ Setup & Run

### **1. Clone the Repository**
```sh
git clone https://github.com/ValeriaGJ6/product-management.git
cd product-management
```

### **2. Backend Setup**

#### **Configure the Database**
- Open `create_products.sql` and execute it on your SQL Server instance to create the database and `Products` table
- Create a new file named `appsettings.Development.json` in the `api/ProductManagement.API` folder
- Add the following content, replacing the connection string with your local SQL Server:
  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Data Source=YOUR_SERVER;Database=ProductManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
    }
  }
  ```

#### **Run the Backend**
```sh
cd api
dotnet restore
cd ProductManagement.API
dotnet run --urls "http://localhost:5200"
```
- The API will be available at `http://localhost:5200`
- Swagger documentation: `http://localhost:5200/index.html`

### **3. Frontend Setup**

#### **Install Dependencies**
```sh
cd frontend
npm install
```

#### **Configure Environment**
- The frontend is already configured to connect to `http://localhost:5200/api`
- If you change the backend port, update `src/environments/environment.ts`

#### **Run the Frontend**
```sh
npm start
```
- The application will be available at `http://localhost:4200`
- It will automatically open in your default browser

## 📱 Usage

1. **Access the application** at `http://localhost:4200`
2. **Create products** using the "New Product" button
3. **View products** in the paginated table
4. **Edit products** by clicking the edit icon
5. **Delete products** by clicking the delete icon (with confirmation)

## 🏗 Project Structure

```
product-management/
├── api/                          # .NET 9 Backend
│   ├── ProductManagement.API/    # Web API layer
│   ├── ProductManagement.Application/ # Business logic
│   ├── ProductManagement.Domain/ # Entities and interfaces
│   └── ProductManagement.Infrastructure/ # Data access
├── frontend/                     # Angular 20 Frontend
│   ├── src/app/
│   │   ├── features/products/    # Product management feature
│   │   ├── shared/              # Reusable components
│   │   └── layout/              # App layout components
│   └── src/environments/        # Environment configuration
└── database/                    # SQL scripts
    └── create_products.sql      # Database setup
```

## 🎯 Technical Highlights

### **Frontend Architecture**
- **Standalone Components**: Modern Angular 20 approach
- **Signal-based State Management**: Reactive and performant
- **Feature-based Structure**: Organized by business domains
- **Shared Components**: Reusable UI elements
- **Reactive Forms**: Type-safe form validation

### **Backend Architecture**
- **Clean Architecture**: Domain-driven design principles
- **CQRS Pattern**: Command and Query separation
- **Repository Pattern**: Data access abstraction
- **Dependency Injection**: Loose coupling and testability

---

Feel free to contribute or open issues for improvements!
