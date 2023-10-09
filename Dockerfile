# Use the official .NET 6 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app
COPY . ./

# Build the application
RUN dotnet publish ./SokobanSolverWebApiCore -c Release -o out

# Use the official ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app
COPY --from=build /app/out ./

# Expose the port your application listens on (e.g., 80 for HTTP)
EXPOSE 80 443

# Start the application
ENTRYPOINT ["dotnet", "SokobanSolverWebApiCore.dll"]
