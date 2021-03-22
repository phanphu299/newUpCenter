# Set ASPNETCORE_ENVIRONMENT
set ASPNETCORE_ENVIRONMENT = Development

#https://www.entityframeworktutorial.net/efcore/cli-commands-for-ef-core-migration.aspx

# Create migration (Build Jip.MigrationTool first)
dotnet ef migrations add {CustomName}

# Generate script
dotnet ef migrations script –p <path to your csproj with migrations> -o $(build.artifactstagingdirectory)\migrations\scripts.sql

#Update
dotnet ef database update

# Revert
20200828091259_InitDatabase

# Test migration 
docker-compose -f ./docker-compose-migration.yml up --build