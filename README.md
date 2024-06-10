## EmployeeManagementFunctions

This project implements a set of Azure Functions for managing employee data.

**Features:**

* Create a new employee (using an HTTP POST request)
* Get employee details (using an HTTP GET request with an employee ID)
* Potentially other functions for managing employees (update, delete, etc.) - not implemented in the current version

**Using the Functions**

The specific URLs for the Azure Functions will depend on your deployment configuration. Once deployed, you can use tools like Postman or curl to interact with the functions.

**Example: Create a new employee**

```bash
POST http://localhost:7071/api/CreateEmployee 
Content-Type: application/json

{
  "name": "John Doe",
  "id": "1234",
  "position": "Software Engineer",
  "salary": 75000,
  "department": "Engineering",
  "address": "123 Main St"
}
```
**Example: Get Employee Record with Employee id** 
```bash
GET  http://localhost:7061/api/employee/9
Content-Type: application/json

{
    "id": "9",
    "position": "Manager",
    "salary": 95000.0,
    "department": "Engineering",
    "name": "Smith John",
    "address": "123 street"
}
```

* The `Employee` class uses C# properties with private setters to enforce data encapsulation.
* The code includes basic error handling using try-catch blocks.

**Next Steps:**

* Implement additional functions for managing employee data (update, delete, etc.).
* Add unit tests for the functions.
* Configure logging and monitoring for the deployed functions.
**Postman Collection for testing the endpoints**
https://www.postman.com/technical-administrator-38638156/workspace/akaler55/documentation/36205369-f189ad8a-1c71-495d-98ef-89f7de1631ae
