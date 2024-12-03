# librarymanagement

### <ins>About</ins>
This Web API project demonstrates Clean Code Architecture, with application layer(api), service layer and repository layer. <br>
This project developed a CRUD functionality for managing the book entity in a library management system.



### <ins>Project structure</ins>


├── backend/<br>
│   ├──Library.Api/ #------------------*Main API project*<br>
│   ├── Library.Service/ #---------------*Core service(business) layer*<br>
│   ├── Library.Reportory/ #-------------*The data access and infrastructure layer*<br>
│   ├── Library.Domain/ #----------------*The data models*<br>
<br><br>
├── tests/<br>
│   ├──Library.Api.Integration.Test/       #---------------- *The Integration test suite*<br>
│   ├──Library.Api.Repository.UnitTest     #---------------- *The unit tests for repository layer*<br>
|   ├──Library.Api.Services.UnitTest       #---------------- *The unit tests for  service layer* <br>
└── README.md<br>
└── .gitignore<br>

### <ins>API Technologies<ins>
1) .NET 8 Web API <br>
2) Minimal API architecture<br>
3) API versioning implementation<br>
4) AutoMapper for object mapping<br>
5) OpenAPI documentation with Swashbuckle (http://localhost:4000/swagger/v1/swagger.json)<br>
6) Middleware for global exception handling<br>
7) Dependency registration and injection for the service and repository layers<br>
8) Generic repository pattern for CRUD operations on entities<br>
9) Generic result pattern (ServiceResult class) in the service layer for consistent result handling<br>
10) Entity Framework Core with SQLite database integration<br>
<br>

### Technologies for Unit Tests and Integration Tests

11) Unit testing with xUnit, including mock tests and in-memory database tests<br>
12) Integration testing with xUnit<br>
13) MSSQL Docker container setup for running the integration test suite<br>
14) Test result and coverage reporting using Coverlet with xUnit<br>
