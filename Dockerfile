FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "digify.csproj" . 
RUN dotnet restore "digify.csproj"
COPY . .
RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_8.x | bash - && apt-get install -yq nodejs build-essential
RUN npm install -g npm
RUN dotnet build "digify.csproj" -c Release

FROM build as publish
RUN dotnet publish "digify.csproj" -c Release

FROM base as final
WORKDIR /app
COPY --from=publish /app/bin/Release/net6.0/publish .
ENTRYPOINT ["dotnet", "digify.dll"]