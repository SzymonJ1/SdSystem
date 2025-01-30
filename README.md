Dokumentacja znajduje się w pliku Readme.pdf

Dodatkowe informacje:

Jeśli w projekcie znajduje się migracja, należy ją usunąć wraz z folderem Migrations. Następnie wykonać Add-Migration 'nazwa' oraz Update-Database.

Konto należy utworzyć poprzez panel rejestracji.

Nadawanie uprawnień należy wykonać poprzez SQL Server Object Explorer (View/SQL Server Object Explorer).

Aby przydzielić rolę koordynatora (Administratora) do konta, należy skopiować  UserId z tabeli SdSystemDb/Tables/dbo.AspNetUsers oraz RoleId z tabeli dSystemDb/Tables/dbo.AspNetRoles i wkleić do tabeli dSystemDb/Tables/dbo.AspNetUserRoles.

