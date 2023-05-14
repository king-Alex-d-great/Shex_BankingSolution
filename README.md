# Shex - Banking Solution

This is a web-based online banking solution developed using the ASP.NET MVC framework. It provides users with a secure platform to manage their bank accounts, view transactions, transfer funds, pay bills, and perform other banking transactions.

## Features
The online banking solution has the following features:

- Secure login and registration system
- Dashboard showing account balances and recent transactions
- View account details and transaction history
- Transfer funds between accounts
- Secure logout
## Requirements
To run this project, you will need the following:

- Visual Studio 2019 or later
- SQL Server Management Studio
- .NET Framework 4.7.2 or later
- Internet Information Services (IIS) installed and configured

## Installation
To install the online banking solution, follow these steps:

- Clone this repository to your local machine using Git or download the ZIP file and extract it to a folder on your machine.
- Open the solution file in Visual Studio.
- Restore the NuGet packages by right-clicking on the solution and selecting "Restore NuGet Packages".
- Create a new database in SQL Server Management Studio and name it "OnlineBanking".
- Open the "Web.config" file in the root folder and update the connection string to match your SQL Server instance and database name.
- Open the Package Manager Console and run the following command: Update-Database
- Build the solution by selecting "Build Solution" from the "Build" menu.
- Run the solution by pressing F5 or selecting "Debug" > "Start Debugging" from the "Debug" menu.
## Usage
To use the online banking solution, follow these steps:

- Register for a new account by clicking the "Register" link on the login page.
- Log in to your account using your email address and password.
- From the dashboard, you can view your account balances and recent transactions.
- Click on the "Accounts" link to view account details and transaction history.
- To transfer funds between accounts, click on the "Transfer" link and select the accounts you want to transfer funds from and to.
- To log out of your account, click on the "Logout" link in the top right corner of the page.

## Roadmap

- Pay bills using a saved payee list: To pay bills, click on the "Pay Bills" link and select a saved payee or add a new one.- 
- Request for new cheque book: To request a new cheque book, click on the "Request Cheque Book" link and enter the required details.

## Contributing
If you find any issues with the online banking solution, please report them in the Issues section of this repository. Pull requests are also welcome for any improvements or bug fixes.

## License
This project is licensed under the MIT License - see the LICENSE file for details.
