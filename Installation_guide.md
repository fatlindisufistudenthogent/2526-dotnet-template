https://dotnet.microsoft.com/en-us/download/dotnet/9.0

windows x64 download

pc herstarten

cd src/Rise.Server

dotnet run

https://localhost:5001

cd

dotnet test Rise.sln

dotnet publish src/Rise.Server/Rise.Server.csproj -c Release -o C:\Users\fatli\Documents\devops-ep3\devops-project-operations-25-26-ep3-fatlindisufistudenthogent\publish

---

install mariadb op je host

& "C:\Program Files\MariaDB 12.3\bin\mysqld.exe" --install                                         
                                                             
Start-Service -Name "MySQL"                                                                        
                                                           
---


# 1. Kolom toevoegen aan C# klasse in Rise.Domain



 Stop-Process -Name "Rise.Server" -Force                                

# 2. Migration aanmaken
cd C:\Users\fatli\Documents\devops-ep3\2526-dotnet-template\src\Rise.Persistence
dotnet ef migrations add NaamVanMigration --startup-project ..\Rise.Server\

& "C:\Program Files\MariaDB 12.3\bin\mysql.exe" -u root -e "DROP DATABASE risedb; CREATE DATABASE risedb CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci; GRANT ALL PRIVILEGES ON risedb.* TO 'riseuser'@'localhost'; FLUSH PRIVILEGES;"

# 3. Lokaal testen
cd C:\Users\fatli\Documents\devops-ep3\2526-dotnet-template
dotnet run --project src/Rise.Server

# 4. Controleer in database
& "C:\Program Files\MariaDB 12.3\bin\mysql.exe" -u root risedb -e "DESCRIBE Product;"

# 5. Push naar GitHub
git add .
git commit -m "feat(db): beschrijving"
git push

---


cd src\Rise.Persistence
Remove-Item Migrations\* -Force
dotnet ef migrations add Initial --startup-project ..\Rise.Server\

(altijd)
--- 

alleen bij wijzigingen dus Wanneer WEL migration nodig:
Kolom toevoegen/verwijderen
Kolomtype wijzigen
Nieuwe tabel/entiteit toevoegen
Relatie tussen tabellen wijzigen

Wanneer GEEN migration nodig:
Tekst in UI wijzigen
CSS aanpassen
Business logica wijzigen
API endpoint toevoegen
Configuratie wijzigen

(enkel lokaal : https:localhost:5001 anders jenkins doet dat)
dotnet build Rise.sln    # controleer of het compileert
dotnet test Rise.sln     # run de tests
dotnet run --project src/Rise.Server  # start de app lokaal

<!-- waarom geen dotnet build test bij migration adden

Omdat dotnet ef migrations add al intern een dotnet build doet. Je ziet het in de output:

Build started...
Build succeeded.
Als de build faalt, faalt ook de migration aanmaak. Dus aparte dotnet build is niet nodig.

En dotnet test hoef je niet te doen voor een migration — de migration verandert alleen de database structuur, niet de business logica die getest wordt. -->