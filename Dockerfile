# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["timc-api/TimcApi.WebAPI/TimcApi.WebAPI.csproj", "timc-api/"]
RUN dotnet restore "timc-api/TimcApi.WebAPI/TimcApi.WebAPI.csproj"

# Copy remaining files and build
COPY . .
WORKDIR "/src/timc-api"
RUN dotnet build "timc-api/TimcApi.WebAPI/TimcApi.WebAPI.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "timc-api/TimcApi.WebAPI/TimcApi.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose port
EXPOSE 8080

# Set entrypoint
ENTRYPOINT ["dotnet", "TimcApi.WebAPI.dll"]
