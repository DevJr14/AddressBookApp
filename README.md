# Address Book

# Configuring AddressBookWebApi 

1. Open solution in Visual Studio 2019
2. Let visual studio to automatically install all packages
3. Configure your connectionString settings in appsettings.json and use the connectionString name in Startup.cs within ConfigureService.
4. Click on Package Manager Console, Run migrations by doing the following
4.1 Add-Migrations
4.2 Update-datebase
4.3 Open your MSSQL and check to verify if the database was indeed created.
5. You are good to start running your API 

# Configuring ApiClient

1. Open the folder in Visual Studio Code
2. Open Terminal
3. cd address-book-api-client
4. Type npm install
4.1 This will install all project packages for you.
5. Run npm start
5.1 This will run the ApiClient
