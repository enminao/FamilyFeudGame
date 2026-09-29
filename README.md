# Family Feud Game

A C# Family Feud game with a Microsoft SQL Server database backend.

## What You Need Before Starting

- Visual Studio
- SQL Server Express
- SQL Server Management Studio (SSMS)
- Git

## Step-by-Step Setup

### 1. Clone the repository

Open Git Bash or any terminal with Git installed and navigate to the folder where you want the project to be stored.

Run:

`git clone https://github.com/enminao/FamilyFeudGame.git`

This downloads the project into a new `FamilyFeudGame` folder.

### 2. Set up the database

1. Open SQL Server Management Studio (SSMS).
2. Connect to your local SQL Server Express instance. The expected server name is `.\SQLEXPRESS`.
3. Open the `setup.sql` file included in this repository.
4. Execute the script.
5. The script creates the `FamilyFeud` database and its required tables.
6. The script only creates the database structure and does not include the existing questions or answers.

### 3. Check your SQL Server instance

The project is configured to use:

`Server=.\SQLEXPRESS`

If your SQL Server instance has a different name:

1. Open `App.config` in Visual Studio.
2. Find the `Server=` part of the connection string.
3. Change it to match your SQL Server instance.

For example:

`Server=YOUR_SERVER_NAME;Database=FamilyFeud;Trusted_Connection=True;TrustServerCertificate=True;`

If your server is already `.\SQLEXPRESS`, no changes are needed.

### 4. Open and run the project

1. Open `FamilyFeud.slnx` from the cloned `FamilyFeudGame` folder.
2. Let Visual Studio restore any required NuGet packages.
3. Press F5 or click the green Start button to build and run the game.

## If You Get an SSL/Certificate Error

If you see an error mentioning a certificate chain when connecting to the database, open `App.config` and make sure the connection string includes:

`TrustServerCertificate=True`

For example:

`Server=.\SQLEXPRESS;Database=FamilyFeud;Trusted_Connection=True;TrustServerCertificate=True;`

## Questions

If something doesn't work, contact the repository owner for help. Common issues are an incorrect SQL Server instance name or missing NuGet packages.
