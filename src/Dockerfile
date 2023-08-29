#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BlogPostManagementService.WebApi/BlogPostManagementService.WebApi.csproj", "BlogPostManagementService.WebApi/"]
COPY ["Architecture/Crosscutting/EmpCore.Crosscutting.DistributedCache/EmpCore.Crosscutting.DistributedCache.csproj", "Architecture/Crosscutting/EmpCore.Crosscutting.DistributedCache/"]
COPY ["Architecture/Infrastructure/EmpCore.Infrastructure.MessageBus/EmpCore.Infrastructure.MessageBus.CAP.csproj", "Architecture/Infrastructure/EmpCore.Infrastructure.MessageBus/"]
COPY ["Architecture/Infrastructure/EmpCore.Infrastructure/EmpCore.Infrastructure.csproj", "Architecture/Infrastructure/EmpCore.Infrastructure/"]
COPY ["Architecture/Domain/EmpCore.Domain/EmpCore.Domain.csproj", "Architecture/Domain/EmpCore.Domain/"]
COPY ["Architecture/Presentation/EmpCore.Api/EmpCore.Api.csproj", "Architecture/Presentation/EmpCore.Api/"]
COPY ["Architecture/Presentation/EmpCore.WebApi.Swagger/EmpCore.WebApi.Swagger.csproj", "Architecture/Presentation/EmpCore.WebApi.Swagger/"]
COPY ["BlogPostManagementService.Application/BlogPostManagementService.Application.csproj", "BlogPostManagementService.Application/"]
COPY ["Architecture/Application/EmpCore.Application/EmpCore.Application.csproj", "Architecture/Application/EmpCore.Application/"]
COPY ["Architecture/QueryStack/EmpCore.QueryStack.Dapper/EmpCore.QueryStack.Dapper.csproj", "Architecture/QueryStack/EmpCore.QueryStack.Dapper/"]
COPY ["BlogPostManagementService.Domain/BlogPostManagementService.Domain.csproj", "BlogPostManagementService.Domain/"]
COPY ["BlogPostManagementService.Persistence/BlogPostManagementService.Persistence.csproj", "BlogPostManagementService.Persistence/"]
COPY ["Architecture/Infrastructure/EmpCore.Persistence.EntityFrameworkCore/EmpCore.Persistence.EntityFrameworkCore.csproj", "Architecture/Infrastructure/EmpCore.Persistence.EntityFrameworkCore/"]
COPY ["BlogPostManagementService.Application/BlogPostManagementService.Contracts.dll", "BlogPostManagementService.Application/"]
RUN dotnet restore "BlogPostManagementService.WebApi/BlogPostManagementService.WebApi.csproj"
COPY . .
WORKDIR "/src/BlogPostManagementService.WebApi"
RUN dotnet build "BlogPostManagementService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogPostManagementService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogPostManagementService.WebApi.dll"]