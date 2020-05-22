FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY /src ./
RUN dotnet restore "WebAPI"
RUN dotnet publish "WebAPI" -c Release -o publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]