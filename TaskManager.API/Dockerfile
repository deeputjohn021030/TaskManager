# Use SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file
COPY TaskManager.API/TaskManager.API.csproj TaskManager.API/

# Restore
RUN dotnet restore "TaskManager.API/TaskManager.API.csproj"

# Copy the rest of the source code
COPY . .

# Build
WORKDIR /src/TaskManager.API
RUN dotnet build -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManager.API.dll"]
