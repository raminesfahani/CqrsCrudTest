#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.API.csproj", "Mc2.CrudTest.Presentation/Server/"]
COPY ["Mc2.CrudTest.Persistence/Mc2.CrudTest.Persistence.csproj", "Mc2.CrudTest.Persistence/"]
COPY ["Mc2.CrudTest.Application/Mc2.CrudTest.Application.csproj", "Mc2.CrudTest.Application/"]
COPY ["Mc2.CrudTest.Presentation/Shared/Mc2.CrudTest.Domain.csproj", "Mc2.CrudTest.Presentation/Shared/"]
RUN dotnet restore "Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.API.csproj"
COPY . .
WORKDIR "/src/Mc2.CrudTest.Presentation/Server"
RUN dotnet build "Mc2.CrudTest.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mc2.CrudTest.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mc2.CrudTest.API.dll"]
