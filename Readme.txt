

http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/migrations-and-deployment-with-the-entity-framework-in-an-asp-net-mvc-application

enable-migrations - creates migrations folder, and initial migration.  Only need to run first time.

add-migration migrationName - Creates a migration with the name "migrationName"

Update-Database -TargetMigration migrationName - Updates DB to a specific migation.  Usefull if we need to add a new migation

project is setup to run the migrations automaticaly on startup.    