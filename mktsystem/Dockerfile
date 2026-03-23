FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# ✅ Use port 10000 for Render
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY mktsystem.sln .

# Copy project files
COPY mktsystem/mktsystem.csproj mktsystem/
COPY mktsystem.application/mktsystem.application.csproj mktsystem.application/
COPY mktsystem.domain/mktsystem.domain.csproj mktsystem.domain/
COPY mktsystem.infrastructure/mktsystem.infrastructure.csproj mktsystem.infrastructure/

# Restore dependencies
RUN dotnet restore mktsystem/mktsystem.csproj

# Copy the rest of the code
COPY . .

# Build and publish
WORKDIR /src/mktsystem
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "mktsystem.dll"]