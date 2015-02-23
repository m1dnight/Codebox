# Codebox
This project is a pastebin clone. It was a project I did for a course during my bachelor at Ghent College (anno 2012).

## Used (required) software
 - Visual Studio 2010
 - MVC 3
 - SQL Server 2012

## Installation
To run this project a database is required. The project assumes the database will be called `codebox_db`. Then, in order for
the project to be able to connect to your database, edit the connection strings provided in `App.config` and `Web.config`.
There are a total of 3 strings to edit.
If you created the database in SQL Server and edited the connection strings you can run the SQL script that is provided in
`CodeBox.Domain/Concrete/ORM`.
The script is named `CodeBoxModelWithProperConstraints.sql`. This will setup the database. Once this is done you can insert
some initial data using the script in the same folder named `insert initial sql data.sql`. Be sure to check the username and
password provided in that file to be able to log in.

This should be all the build and run the project.
