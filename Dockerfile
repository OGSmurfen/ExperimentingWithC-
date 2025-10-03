# Use official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore
COPY ExperimentingWithC#.sln ./
COPY WebApplication1/WebApplication1.csproj WebApplication1/
COPY NoWindowDraw_TimerImpl/NoWindowDraw_TimerImpl.csproj NoWindowDraw_TimerImpl/
RUN dotnet restore WebApplication1/WebApplication1.csproj

# Copy everything else and build
COPY . .
RUN dotnet publish WebApplication1/WebApplication1.csproj -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]
