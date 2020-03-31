FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as build

WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-alpine
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DockerDbup.dll"]