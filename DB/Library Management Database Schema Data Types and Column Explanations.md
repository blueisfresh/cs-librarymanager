
This document provides an explanation of each column in the library management system database schema using MS SQL data types, based on the final schema design.

---

## Tables and Columns

### 1. **TblStudent**
Stores information about each student who can borrow books.

| Column           | Data Type     | Purpose                                |
|------------------|---------------|----------------------------------------|
| **LibraryCardNum** | INT         | Unique identifier for each student, representing their library card number. `INT` is used as a numeric identifier. |
| **FirstName**      | NVARCHAR(50) | Stores the student's first name to support multilingual characters. |
| **LastName**       | NVARCHAR(50) | Stores the student's last name to support multilingual characters. |

---

### 2. **TblBook**
Stores details about each book available in the library.

| Column             | Data Type     | Purpose                                    |
|--------------------|---------------|--------------------------------------------|
| **BookNum**        | CHAR(10)      | Unique identifier for each book copy, in the format `xxxxx-yyyy`. `CHAR(10)` is used as it’s a fixed-length format. |
| **Title**          | NVARCHAR(100) | Title of the book, supporting multilingual characters. |
| **Author**         | NVARCHAR(100) | Author(s) of the book, supporting multilingual names. |
| **Publisher**      | NVARCHAR(100) | Publisher of the book. `NVARCHAR` is used to support potential special characters in publisher names. |
| **ISBN**           | NCHAR(13)     | International Standard Book Number (fixed length). `NCHAR` is used to support international encoding. |
| **PublicationPlace** | NVARCHAR(50) | Place where the book was published, supporting international place names. |
| **PublicationDate** | DATETIME    | Original publication date of the book. `DATETIME` is used for compatibility with other databases and future flexibility if time information is needed. |

---

### 3. **TblBorrow**
Tracks borrowing transactions linking students with specific copies of books.

| Column             | Data Type     | Purpose                                      |
|--------------------|---------------|----------------------------------------------|
| **BorrowID**       | INT           | Unique identifier for each borrow record. `INT` is used as a numeric primary key. |
| **StudentLibraryCardNum** | INT   | Links to the student borrowing the book. `INT` as a foreign key referencing `TblStudent.LibraryCardNum`. |
| **BookBookNum**    | CHAR(10)      | Links to the borrowed book copy. `CHAR(10)` matches the format and length of `TblBook.BookNum`. |
| **BorrowDate**     | DATETIME      | Date the book was borrowed. `DATETIME` provides flexibility for tracking date and time. |
| **ReturnDate**     | DATETIME      | Date the book was returned, ensuring accurate tracking. |
| **DueDate**        | DATETIME      | Latest date by which the book should be returned, enabling precise due date tracking. |

---

## Summary Table of MS SQL Data Types and Purpose

| Column            | Data Type     | Purpose                           |
|-------------------|---------------|-----------------------------------|
| **LibraryCardNum**| INT           | Unique ID for each student        |
| **FirstName**     | NVARCHAR(50)  | First name of the student         |
| **LastName**      | NVARCHAR(50)  | Last name of the student          |
| **BookNum**       | CHAR(10)      | Unique ID for each book copy      |
| **Title**         | NVARCHAR(100) | Title of the book                 |
| **Author**        | NVARCHAR(100) | Author(s) of the book             |
| **Publisher**     | NVARCHAR(100) | Publisher of the book             |
| **ISBN**          | NCHAR(13)     | ISBN of the book                  |
| **PublicationPlace** | NVARCHAR(50)| Place of publication              |
| **PublicationDate** | DATETIME    | Publication date of the book      |
| **BorrowID**      | INT           | Unique ID for each borrow record  |
| **BorrowDate**    | DATETIME      | Date the book was borrowed        |
| **ReturnDate**    | DATETIME      | Date the book was returned        |
| **DueDate**       | DATETIME      | Return deadline for the borrowed book |

---

This final schema design uses MS SQL Server data types that ensure compatibility, multilingual support, and precise data tracking. Each field's data type is chosen for its specific use case within the library management system.
