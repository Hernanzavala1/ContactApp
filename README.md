# ContactApp
This project's main goal was to allow for users to manage their contacts via intuitive functionallity and user interface. 
The application uses SQL server so it is required for you to update the connection string prior to running the application.
This can be done by clicking on View -> SQL Server Object Explorer -> Your PC name -> right click on it -> properties. Once you are in the properties, the connection string will be present. This string must be pasted into appsettings.json file under connection strings. You must give the string a name that you see fit. This connection name will be used in the Startup.cs file, it is necessary in the configureServices method. The database will be seeded with 2 records when the model is created and migrations are taking place. 
Features and usability of the application:
With this application, one is able to do CRUD operations. One can create a new contact and store it in the database, one can update/edit a specific contact, search a contact based on first and last name or by city and lastly one can delete a contact. All of these operations are fairly simple and intuitive for any user of the application.

There is very limited amount of css and much styling as my taste in style will vary from those of the customer for whom i may build this application.
