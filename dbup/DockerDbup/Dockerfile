FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DockerDbup.dll"]