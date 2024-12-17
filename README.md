# Library Management System

## Overview

The Library Management System is a WPF application designed to manage a library's books, students, and borrowing/returning activities. The system includes functionalities for adding, editing, and deleting books and students, as well as importing and exporting data.

## Project Structure

The project is organized into several directories:

- **LibraryManagement**: Contains the main application code.

  - **App.xaml**: Application definition.
  - **App.xaml.cs**: Application logic.
  - **MainWindow\.xaml**: Main window definition.
  - **MainWindow\.xaml.cs**: Main window logic.
  - **Components**: Contains reusable components.
  - **ViewModels**: Contains the view models for the application.
  - **Views**: Contains the views for the application.
  - **Resources**: Contains resource files.
  - **LibraryManagement.csproj**: Project file for the main application.

- **LibraryManagementSystem**: Contains the core library management logic.

  - **Data**: Contains data access classes.
  - **Models**: Contains data models.

- **LibraryManagementSystem.Tests**: Contains unit tests for the core library management logic.

- **DB**: Contains SQL scripts for database initialization and data insertion.

## Key Files and Classes

### Main Application

- **App.xaml**: Defines the application resources and startup behavior.
- **App.xaml.cs**: Contains the application startup logic.
- **MainWindow\.xaml**: Defines the main window layout.
- **MainWindow\.xaml.cs**: Contains the main window logic.

### ViewModels

- **BooksViewModel.cs**: Manages the book-related operations.
- **StudentsViewModel.cs**: Manages the student-related operations.
- **BorrowReturnViewModel.cs**: Manages the borrowing and returning operations.
- **AddBooksViewModel.cs**: Manages the add/edit book operations.
- **AddStudentViewModel.cs**: Manages the add/edit student operations.
- **ImportExportViewModel.cs**: Manages the import/export operations.
- **StatisticsViewModel.cs**: Manages the statistics operations.

### Views

- **AddBooksWindow\.xaml**: Defines the layout for adding/editing books.
- **AddStudentWindow\.xaml**: Defines the layout for adding/editing students.
- **DeleteBookWindow\.xaml**: Defines the layout for deleting books.
- **DeleteStudentWindow\.xaml**: Defines the layout for deleting students.
- **BooksPage.xaml**: Defines the layout for displaying books.
- **StudentsPage.xaml**: Defines the layout for displaying students.
- **BorrowReturnPage.xaml**: Defines the layout for borrowing/returning books.
- **StatisticsPage.xaml**: Defines the layout for displaying statistics.

### Data Access

- **BookRepository.cs**: Handles data operations for books.
- **StudentRepository.cs**: Handles data operations for students.
- **BorrowRepository.cs**: Handles data operations for borrowing/returning books.

### Models

- **Book.cs**: Represents a book.
- **Student.cs**: Represents a student.
- **Borrow\.cs**: Represents a borrowing record.

### Database

- **LibraryManagementDB.sql**: SQL script for creating the database.
- **DB-initializationScript.sql**: SQL script for initializing the database.
- **InsertValuesScript.sql**: SQL script for inserting initial data.

## Installation Guide

### Prerequisites

- **.NET 8.0 SDK**
- **SQL Server** (or LocalDB for development)
- **Visual Studio** (recommended for development)

### Steps

1. **Clone the repository**:

   ```sh
   git clone https://github.com/your-repo/library-management-system.git
   cd library-management-system
   ```

2. **Set up the database**:

   - Open **SQL Server Management Studio (SSMS)** or your preferred SQL tool.
   - **Create a new database** named `LibraryManagement` (or the name configured in your connection string).
   - Navigate to the `DB` folder in the project directory.
   - Execute the SQL scripts in the following order:
     1. **LibraryManagementDB.sql** – Creates the database schema.
     2. **DB-initializationScript.sql** – Initializes necessary tables.
     3. **InsertValuesScript.sql** – Inserts initial data into the database.

3. **Build the project**:

   ```sh
   dotnet build LibraryManagement.sln
   ```

4. **Run the application**:

   ```sh
   dotnet run --project LibraryManagement
   ```

---

