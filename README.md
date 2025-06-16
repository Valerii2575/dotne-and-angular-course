# dotne-and-angular-course
We are going to do is build an example of a daiting app, a social kind of apps such as registration and login

## Setting up VS Code for work

I'm going to install some extensions for development

- C# Dev Kit: C# Dev Kit helps you manage your code with a solution explorer and test your code with integrated unit test discovery and execution, elevating your C# development experience wherever you like to develop (Windows, macOS, Linux, and even in a Codespace).
- Material Icon Theme: Get the Material Design icons into your VS Code
- NuGet Gallery: The NuGet Gallery extension streamlines the process of installing and uninstalling NuGet packages, making it more 
- SQLite: Explore and query SQLite databases.

Install global dotnet EF
- dotnet tool install --global dotnet-ef -varsion [8.0.4]
- check version of EF - dotnet ef -v


## Create ASP.NET API project

run command
dornet new webapi -controllers -n API

## Adding EntityFramework

- Add Model Entity with call - AppUser
- Add dependensies:
    - Microsoft.EntityFrameworkCore.SqlLite,
    - Microsoft.EntityFrameworkCore.Design

- Add new folder Data for DbContext
    A DbContext instance represents a session with the database and can be use to query and save instances of entyties.


## Adding Bootstrap and Font-Awesome

visit site - ngx-bootstrap

User the Angular CLI for updating Angular project

ng add ngx-bootstrap
or
npm install ngx-bootastrap@11 bootstrap font-awesome

addin angular.json 

```
    "styles": [
              "node_modules/bootstrap/dist/css/bootstrap.min.css",
              "node_modules/font-awesome/css/font-awesome.min.css",
              "src/styles.css"
            ],
```