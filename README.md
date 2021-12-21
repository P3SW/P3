# P3

This is P3 project for SW3-04 group. This project is a conversion dashboard, that should help Netcompany during data conversions.

## Setup Microsoft SQL server

This solution requires a Microsoft SQL database, here is how to setup the server

**Windows and Linux**

1. Install the server using the [link](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
2. Make sure that you have a user with username `sa` and password `Password123`

**MacOS**

1. Install Docker.
2. Execute the following command `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest` in your terminal.
3. The Microsoft SQL server has now been created.

To access the server, either VSCode, DataGrip, Azure Data Studio etc. can be used.

### Create conversion database

1. Create a aes user by running `Database/CREATE_aes_LOGIN.sql`
2. To fill the database with data from a conversion, run the `Database/CUSTOM.sql`script. 

IMPORTANT: make sure the file path on line 7 and 9 matches with your database's path.

## Data patches

In order to run the application, data patches have to be made.

- Run `ManagerTrackingFixer` project.

## Running the Application

To run the program, dotnet 5.0 is required, it can be installed [here](https://dotnet.microsoft.com/en-us/download/dotnet/5.0).

**Using Jetbrains Rider**

1. Open the `P3ConversionDashboard.sln`in JetBrains Rider.
2. Run the Project run configuration.

**Using Terminal**

1. Open 2 terminal windows in the code directory
2. In the first terminal window run the following:
  1. `cd BlazorApp/`
  2. `dotnet run`
3. In the second terminal window
  1. `cd DataStreamingSimulation/`
  2. `dotnet run` 

## Running Tests

In order to run tests, your file path to the database should be the same as your database's path, this should be done in the following files:

- `Test/BlazorBackendTest/NEW_CREATE_ANS_DB_P3_TEST.sql`
- `Test/DataStreamingTest/NEW_CREATE_ANS_DB_P3_TEST.sql`
- `Test/SQLDependencyListenerTest/NEW_CREATE_ANS_DB_P3_TEST.sql`

After that you can run the tests, by executing the `Test` project.
