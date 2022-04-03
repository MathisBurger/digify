FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ENV APT_KEY_DONT_WARN_ON_DANGEROUS_USAGE=1
RUN apt-get update -yq 
RUN apt-get install curl gnupg -yq 
RUN curl -sL https://deb.nodesource.com/setup_13.x | bash -
RUN apt-get install -y nodejs

WORKDIR /src
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