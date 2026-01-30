# ===== Build Stage =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files first (for better caching)
COPY habitsBuilderBackEnd.sln ./
COPY habitsBuilderBackEnd/*.csproj ./habitsBuilderBackEnd/

# Restore dependencies
RUN dotnet restore

# Copy all source code
COPY habitsBuilderBackEnd/ ./habitsBuilderBackEnd

WORKDIR /src/habitsBuilderBackEnd

# Publish to /app directory
RUN dotnet publish -c Release -o /app

# ===== Runtime Stage =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published files
COPY --from=build /app .

# Expose API port
EXPOSE 5000

# Start the application
ENTRYPOINT ["dotnet", "habitsBuilderBackEnd.dll"]
