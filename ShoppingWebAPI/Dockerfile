FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV PORT=10000
EXPOSE $PORT

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# build context 已經是專案資料夾了，直接 COPY .
COPY . .
WORKDIR /src

# restore / publish
RUN dotnet restore ShoppingWebAPI.csproj
RUN dotnet publish ShoppingWebAPI.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShoppingWebAPI.dll", "--urls=http://*:$PORT"]
