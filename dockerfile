FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /workspace-service-app

EXPOSE 80
EXPOSE 5019

# COPY csproj and restore as distinct layers
COPY ./*csproj ./
RUN dotnet restore

# COPY everything else and build app
COPY . .
RUN dotnet publish -c Release -o out

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /workspace-service-app
COPY --from=build /workspace-service-app/out .
ENTRYPOINT ["dotnet", "insightflow-workspace-service.dll"]