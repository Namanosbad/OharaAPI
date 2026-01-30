FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Ohara.API.Internal/Ohara.API.Internal.csproj", "Ohara.API.Internal/"]
COPY ["Ohara.API.Application/Ohara.API.Application.csproj", "Ohara.API.Application/"]
COPY ["Ohara.API.Ioc/Ohara.API.Ioc.csproj", "Ohara.API.Ioc/"]
COPY ["Ohara.API.Shared/Ohara.API.Shared.csproj", "Ohara.API.Shared/"]

RUN dotnet restore "Ohara.API.Internal/Ohara.API.Internal.csproj"

COPY . .
WORKDIR "/src/Ohara.API.Internal"
RUN dotnet build "Ohara.API.Internal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ohara.API.Internal.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ohara.API.Internal.dll"]