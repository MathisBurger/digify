FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:alpine AS node_base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY --from=node_base . .
COPY "digify.csproj" . 
RUN dotnet restore "digify.csproj"
COPY . .

RUN dotnet build "digify.csproj" -c Release

FROM build as publish
RUN dotnet publish "digify.csproj" -c Release

FROM base as final
WORKDIR /app
COPY --from=publish /app/bin/Release/net6.0/publish .
ENTRYPOINT ["dotnet", "digify.dll"]