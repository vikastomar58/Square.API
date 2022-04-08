# https://docs.docker.com/engine/examples/dotnetcore/#build-and-run-the-docker-image 
# refence url
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Squares.API/*.csproj ./Squares.API/
COPY Squares.API.Domain/*.csproj ./Squares.API.Domain/
COPY Squares.API.DataLayer/*.csproj ./Squares.API.DataLayer/
COPY Squares.API.Test/*.csproj ./Squares.API.Test/ 

RUN dotnet restore

# Copy everything else and build
COPY Squares.API/. ./Squares.API/
COPY Squares.API.Domain/. ./Squares.API.Domain/
COPY Squares.API.DataLayer/. ./Squares.API.DataLayer/

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Squares.API.dll"]
