# 📚 Library Management System

A simple console-based library management system built with C# and .NET 8.0.

## Features

- Add, list, delete, and search books
- Add, list, and delete members
- Borrow and return books with due date tracking
- Late return detection
- Data persistence with JSON file storage

## Project Structure

```
LibraryManagementSystem/
├── Models/
│   ├── Book.cs
│   ├── Member.cs
│   └── BorrowRecord.cs
├── Services/
│   └── LibraryService.cs
├── Data/
│   └── JsonRepository.cs
├── Program.cs
└── README.md
```

## How to Run

1. Clone the repository
2. Open in Visual Studio
3. Press `Ctrl+F5` to run

## Technologies

- C# / .NET 8.0
- System.Text.Json