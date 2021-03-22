#Build
From mcr.microsoft.com/dotnet/core/sdk:3.1 as build
workdir /app
copy *.csproj ./
run dotnet restore

copy . ./
run dotnet publish -c release -o /app
#Run
From mcr.microsoft.com/dotnet/core/aspnet:3.1 as final
workdir /app
copy --from=build /app .
entrypoint ["dotnet","AuthenticationService.dll"] 


