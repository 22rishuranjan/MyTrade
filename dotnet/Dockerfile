# Use the official .NET 8 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file
COPY MyTrade.sln ./

# Copy project files individually for all projects in the solution
COPY src/MyTrade.Api/*.csproj ./src/MyTrade.Api/
COPY src/MyTrade.Application/*.csproj ./src/MyTrade.Application/
COPY src/MyTrade.Domain/*.csproj ./src/MyTrade.Domain/
COPY src/MyTrade.Infrastructure/*.csproj ./src/MyTrade.Infrastructure/

# Restore dependencies for the solution
RUN dotnet restore

# Copy the entire project into the container
COPY . .

# Set the working directory to the API project
WORKDIR /app/src/MyTrade.Api

# Build and publish the application
RUN dotnet publish -c Release -o /out

# Use the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

# Expose the port your app runs on
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "MyTrade.Api.dll"]
