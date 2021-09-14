# MartianRobots Documentation

Martian Robots is a Web API Project created in .NET Core 3.1
Key Features:
- Clean Architecture (N-Layer) solution
- Design Patterns such as Repository, Unit of Work, Data Mapping (Automapper)
- Entity Framework Core using Code First approach for the Database model through Migrations
- Implementation of a custom ILogger Interface
- Make use of Helpers, Enumerations and Data Transfer Objects
- Unit and Integration tests using xUnit framework, and Mocks with MOQ
- Complete deployment on Azure, ready to use

The API exposes a method to process a list of robots movements, perform all necessary calculation as instructed in the exercise,
and save all records in the database for statistic purposes. Lastly it returns an output with the desired information described 
as the Sample Output.

There is another method exposed to extract all robots movements for statistics usage. There is room for more methods, making use of 
what is saved in the database, such as the Robot Scents, and a history of each Robot Movement on its Grid.

Application Functionality:
--------------------------
1) Once the Application is running, the API will be expecting a JSON Payload containing the input details, as described in the Sample Input
- Note: It is assumed that the input is already validated at the client side, which means the information is consistent for usage in 
the backend. Client side validation is more performant and it prevents unnecessary postbacks.
2) The input will then be processed according to the exercise expectations, saving every robot movement in the database. It also keeps
track of the robot scents and the grids used for each robot
3) After all calculations are done, the output is returned to the client side.

The Application can be ran locally, and it is also available in Azure.
-- Local Endpoints --
http://localhost:5000/api/martian/input (Body in JSON)
http://localhost:5000/api/martian/GetRobotMovements

-- Azure Endpoints --
https://martianrobotsapi.azure-api.net/v1/api/Martian/Input (Body in JSON)
https://martianrobotsapi.azure-api.net/v1/api/Martian/GetRobotMovements



SPECIFIC DETAILS FOR THE APPLICATION

---------------------------------------------------
- JSON INPUT Payload example:
---------------------------------------------------
{    
	"GridCoordinates":
    {
      "X": 5,
      "Y": 3
    },
    "RobotOperations":
    [
        {
            "Name": "Robot 1",
            "Position": 
            {
                "X": 1,
                "Y": 1
            },
            "Orientation": "E",
            "Instructions": "RFRFRFRF"            
        },
        {
            "Name": "Robot 2",
            "Position": 
            {
                "X": 3,
                "Y": 2
            },
            "Orientation": "N",
            "Instructions": "FRRFLLFFRRFLL"            
        },
        {
            "Name": "Robot 3",
            "Position": 
            {
                "X": 0,
                "Y": 3
            },
            "Orientation": "W",
            "Instructions": "LLFFFRFLFL"            
        }
        
    ]
}


---------------------------------------------------
- JSON OUTPUT Payload example:
---------------------------------------------------
{
   "robotResult": 
       [
            {
                "name": "Robot 2",
                "gridCoordinates": null,
                "position": {
                    "x": 3,
                    "y": 3
                },
                "orientation": "N",
                "instruction": "",
                "status": "LOST"
            }
       ]
}


The Database model consists of 5 tables:
-Robot
-GridCoordinates
-Position
-RobotMovements
-RobotScents

The Application has a local database stored in the API Project (.mdf file), designed specially to make it simple to clone the project 
in github and make it work immediatelly with no further configurations, it can be easily replaced by a local SQL Server instance 
with the appropriate connection string mapping, if needed.

The Azure Application is deployed in Azure App Service, with the needed dependencies to connect to a SQL Database stored in the
same Resource Group, with an encrypted connection string. The REST API is configured through the API Management Service


Unit Testing and Integration Testing was also developed for the application, using the xUnit framework and Mocks through the MOQ library.
Each layer has its own Test Project, and some tests were created for each component:
-Entities Instantiation
-API Flow in Controller
-Database Integration Tests
-InMemoryDatabase to mock Robot Operations


Because of time constraints I didn't have the opportunity to build also a WEB Project to act as the Front End, in which I wanted
to implement the proper input validation before sendind data to the API Endpoint. I could have built this using Razor Pages with
a simple layout made with Bootstrap, and jQuery to handle validations with regular expressions on each textbox for the user input.
For this exercise I am assuming that the validation is already done by the time the endpoint is consumed.