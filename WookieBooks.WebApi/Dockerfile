#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["WookieBooks.WebApi/WookieBooks.WebApi.csproj", "WookieBooks.WebApi/"]
COPY ["WookieBooks.Domain/WookieBooks.Domain.csproj", "WookieBooks.Domain/"]
COPY ["WookieBooks.Framework/WookieBooks.DomainFramework.csproj", "WookieBooks.Framework/"]
COPY ["WookieBooks.Data/WookieBooks.Data.csproj", "WookieBooks.Data/"]
RUN dotnet restore "WookieBooks.WebApi/WookieBooks.WebApi.csproj"
COPY . .
WORKDIR "/src/WookieBooks.WebApi"
RUN dotnet build "WookieBooks.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WookieBooks.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WookieBooks.WebApi.dll"]