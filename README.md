# emunation-api

dotnet ef migrations add InitialCreate -s . -p ../Emunation.Data -c DataContext -o Migrations

dotnet ef database update -s . -p ../Emunation.Data