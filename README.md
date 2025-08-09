# TodoAPI: A RESTful API for Managing Tasks

A simple yet robust RESTful API built with ASP.NET Core for managing a to-do list. This project demonstrates core backend development principles including CRUD operations, data persistence with Entity Framework Core, and automated unit testing.

---

## Features

-   **Full CRUD Functionality:** Create, Read, Update, and Delete to-do items.
-   **Data Persistence:** Data is saved to a persistent database using Entity Framework Core, ensuring that tasks are not lost when the application restarts.
-   **Unit Tested:** The application's business logic is verified with a suite of unit tests using the xUnit framework to ensure reliability and correctness.
-   **Dependency Injection:** Follows modern software design principles by using Dependency Injection to manage dependencies like the database context.
-   **Swagger Documentation:** Includes an interactive Swagger UI for easy exploration and testing of the API endpoints.

---

## Technologies Used

-   **Backend:** C#, ASP.NET Core 8
-   **Database:** Entity Framework Core with SQLite (for local development)
-   **Testing:** xUnit
-   **API Documentation:** Swashbuckle (Swagger)

---

## Getting Started

### Prerequisites

-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   A code editor like Visual Studio or VS Code

### Setup and Installation

1.  **Clone the repository:**
    ```bash
    git clone [Your GitHub Repository URL]
    ```

2.  **Navigate to the project directory:**
    ```bash
    cd TodoAPI
    ```

3.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```

4.  **Run the database migrations:**
    The project is configured to use a simple SQLite database. The following command will create the `TodoApp.db` file and set up the necessary tables.
    ```bash
    dotnet ef database update
    ```

5.  **Run the application:**
    ```bash
    dotnet run
    ```
    The API will be available at `https://localhost:7212` (or a similar port). You can access the interactive Swagger documentation by navigating to `https://localhost:7212/swagger`.

---

## API Endpoints

All endpoints are relative to the base URL (e.g., `https://localhost:7212/api/todos`).

### Todos

-   **`GET /api/todos`**
    -   **Description:** Retrieves a list of all to-do items.
    -   **Response:** `200 OK` with an array of to-do objects.

-   **`POST /api/todos`**
    -   **Description:** Creates a new to-do item.
    -   **Request Body:**
        ```json
        {
          "text": "Learn to deploy to Azure"
        }
        ```
    -   **Response:** `201 Created` with the newly created to-do object.

-   **`PUT /api/todos/{id}`**
    -   **Description:** Updates an existing to-do item.
    -   **Request Body:**
        ```json
        {
          "text": "Deploy the app to Azure",
          "done": true
        }
        ```
    -   **Response:** `200 OK` with the updated to-do object.

-   **`DELETE /api/todos/{id}`**
    -   **Description:** Deletes a specific to-do item.
    -   **Response:** `204 No Content`.
