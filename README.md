# librarymanagement

### <ins>About</ins>
###### This is a Web API project demonstrating Clean Code Architecture for CRUD functionality within a library management system for managing books.



### <ins>Project structure</ins>


├── backend/<br>
│   ├──Library.Api/ #------------------*Main API project*<br>
│   ├── Library.Service/ #---------------*Core business layer*<br>
│   ├── Library.Reportory/ #-------------*The data access and infrastructure layer*<br>
│   ├── Library.Domain/ #----------------*The data models*<br>
<br><br>
├── tests/<br>
│   ├──Library.Api.Integration.Test/       #---------------- *The Integration test suite*<br>
│   ├──Library.Api.Repository.UnitTest     #---------------- *The unit tests for repository layer*<br>
|   ├──Library.Api.Services.UnitTest       #---------------- *The unit tests for  service layer* <br>
└── README.md<br>
└── .gitignore<br>

### <ins>Technologies<ins>
* .Net 8 Web Api,<br> 
* Minimal API <br> 
* API versioning <br>
* automapper
* OpenAPI Swashbuckle <br> 
* Global Exception handler middleware <br>
* Dependecy registration and injuction for service layer and repository layer <br>
* Generic repository layer for CURD operation of entities<br>
* Entity Framework Core <br> 
* sqllite database <br>
* xUnit mock tests <br>
* xUnit integration tests<br>
* MSSQL backend running in a Docker container <br>
* xUnit test results and coverage using coverlet
