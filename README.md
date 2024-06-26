# Sellers

Manage shops, districts and vendors.

This is a test project I've implemented for a technical interview.

## Technologies

* Requirements
  - Microsoft Sql Server
  - .NET 8
* Frontend
  - React TypeScript
  - tailwindcss
  - axios
* Backend
  - .NET Core Web API
  - Dapper

## Installation

1. Clone the repository
2. Execute **Sellers DB Script** sql query file from **db** folder in Sql Server Management Studio
3. Open **SellersAPI** solution file
   - update *Connection String* with your Sql Server in **appsettings.json** file located in **SellersAPI** project
   - run the project
4. Open **sellers-react** in Visual Studio Code. In **sellers-react** directory run 
   - `npm install` - installs all dependencies
   - `npm start` - runs the client app

## License

[MIT](https://choosealicense.com/licenses/mit/)
