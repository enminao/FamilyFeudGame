# Family Feud Game

A C# Family Feud game with a Microsoft SQL Server database backend.

## What You Need Before Starting

- **Visual Studio** (Community edition is free) — download from visualstudio.microsoft.com if you don't have it
- **SQL Server Express** — download from microsoft.com/sql-server/sql-server-downloads if you don't have it
- **SQL Server Management Studio (SSMS)** — download from the same Microsoft page above
- **Git** — download from git-scm.com if you don't have it

## Step-by-Step Setup

### 1. Clone the repository

Open Git Bash (or any terminal with Git installed), navigate to the folder where you want the project to live, and run:

```
git clone https://github.com/enminao/FamilyFeudGame.git
```

This downloads a full copy of the project into a new `FamilyFeudGame` folder.

### 2. Set up the database

1. Open **SQL Server Management Studio (SSMS)**
2. Connect to your local SQL Server instance (the "Server name" field is usually something like `.\SQLEXPRESS`)
3. Open the setup script included in this repo: `setup.sql`
4. Click **Execute** to run it — this creates the database and its tables

### 3. Check your SQL Server instance name

1. In SSMS, look at what you typed into the **Server name** field when connecting — that's your instance name
2. If it's exactly `.\SQLEXPRESS`, you're already good — skip to step 4
3. If it's something else, open `App.config` in the project (in Visual Studio) and update the `Server=` part of the connection string to match your instance name

### 4. Open and run the project

1. In the cloned `FamilyFeudGame` folder, double-click `FamilyFeud.sln` to open it in Visual Studio
2. Let Visual Studio restore any missing NuGet packages (it usually does this automatically — if not, right-click the solution in **Solution Explorer** and choose **Restore NuGet Packages**)
3. Press **F5** (or click the green **Start** button) to build and run the game

## If You Get an SSL/Certificate Error

If you see an error mentioning "certificate chain" when connecting to the database, open `App.config` and make sure the connection string includes `TrustServerCertificate=True`, like this:

```
Server=.\SQLEXPRESS;Database=FamilyFeud;Trusted_Connection=True;TrustServerCertificate=True;
```

## Questions

If something doesn't work, message enminao directly rather than guessing — it's usually a quick fix (wrong instance name or a missing package).
