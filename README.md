# Site De Cadastro

Project developed in C# with ASP.NET Core MVC, using Entity Framework Core and PostgreSQL.

It's a basic application for user registration and login, with data stored in a PostgreSQL database.

## First stage 
### Authentication System
## User Registration
## User Login
## User Logout
## Password Hashing
## Cookie Authentication

## Installation Instructions
### Prerequisites
## C# 
## PostgreSQL
## .NET SDK

### Packages
## Microsoft.EntityFrameworkCore
## Microsoft.EntityFrameworkCore.Design
## Npgsql.EntityFrameworkCore.PostgreSQL
## dotnet-ef

### Steps

## Install the required packages:

```
dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

## Install the Entity Framework Core CLI:

```
dotnet tool install --global dotnet-ef
```
## Add your PostgreSQL connection string to appsettings.json:
```
    "DefaultConnection": "Host=localhost;Port=5432;Database=SiteCadastro;Username=postgres;Password=YOUR_PASSWORD"

```

## Create the database using the migrations:

```
dotnet ef database update
```

## Run the application:

```
dotnet watch
```
