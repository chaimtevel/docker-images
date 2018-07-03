FROM microsoft/dotnet:2.1-sdk-alpine3.7 as build

WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet:2.1-runtime-alpine
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DockerDbup.dll"]