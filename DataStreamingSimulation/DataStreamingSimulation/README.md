# Important

First time using this solution, follow these steps:
1. Make a "setup.txt" file in the following directory: **DataStreamingSimulation/bin/Debug/net5.0**
2. This setup file should contain the following 2 lines of code:
    
        Server=localhost,1433;Database=ANS_CUSTOM_2; User ID=sa; Password=Password123;Trusted_Connection=False
        Server=localhost,1433;Database=ANS_DB_P3; User ID=sa; Password=Password123;Trusted_Connection=False

    - **Server**: the server where the Microsoft SQL database server is hosted.
    - **Database**: the first database is where the program will query from, and the second database is where the input will be.
    - **User ID**: the user ID for the Microsoft SQL database server, the standard is 'sa'.
    - **Password**: The password for the Microsoft SQL database server.
    - **Trusted_Connection**: should stay "false".