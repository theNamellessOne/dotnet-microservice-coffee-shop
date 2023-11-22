FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /CoffeeService
EXPOSE 80

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /CoffeeService
COPY --from=build-env /CoffeeService/out .
ENTRYPOINT ["dotnet", "CoffeeService.dll"]