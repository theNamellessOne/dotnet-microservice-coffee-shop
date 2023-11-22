FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /UserService
EXPOSE 80

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /UserService
COPY --from=build-env /UserService/out .
ENTRYPOINT ["dotnet", "UserService.dll"]