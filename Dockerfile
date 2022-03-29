FROM mcr.microsoft.com/dotnet/core/aspnet:6.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:6.0-buster AS build
WORKDIR /src
COPY "digify.csproj" . 
RUN dotnet restore "digify.csproj"
COPY . .
RUN dotnet build "digify.csproj" -c Release /app/build

FROM build as publish
RUN dotnet publish "digify.csproj" -c Release /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "digify.dll"]