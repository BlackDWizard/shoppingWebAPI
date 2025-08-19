# -------------------------------
# Base image for runtime
# -------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# -------------------------------
# Build stage
# -------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 假設 build context 已經是專案資料夾
COPY . .

# Restore + Publish
RUN dotnet restore ShoppingWebAPI.csproj
RUN dotnet publish ShoppingWebAPI.csproj -c Release -o /app/publish /p:UseAppHost=false

# -------------------------------
# Final stage
# -------------------------------
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# 使用 Render 提供的 $PORT 綁定，避免 timeout
ENTRYPOINT ["dotnet", "ShoppingWebAPI.dll", "--urls=http://*:$PORT"]
