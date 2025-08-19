FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV PORT=10000
EXPOSE $PORT

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ShoppingWebAPI ./ShoppingWebAPI
WORKDIR /src/ShoppingWebAPI
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShoppingWebAPI.dll", "--urls=http://*:$PORT"]
