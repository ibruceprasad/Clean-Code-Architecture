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

### <ins>Technologies<ins>
* .Net 8 Web Api,<br> 
* Minimal API <br> 
* API versioning <br>
* automapper
* OpenAPI Swashbuckle Spec- http://localhost:4000/swagger/v1/swagger.json  <br> 
* Global Exception handler middleware <br>
* Dependecy registration and injuction for service layer and repository layer <br>
* Generic repository layer for CURD operation of entities<br>
* Generic Result pattern(class -ServiceResult) in service layer to return results <br> 
* Entity Framework Core <br> 
* sqllite database <br>
* xUnit mock tests for service layer and repository layer <br>
* xUnit integration tests<br>
* Integration test suite spin off MSSQL Docker container<br>
* xUnit test results and coverage using coverlet
