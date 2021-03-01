#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["snap7-to-prometheus.csproj", ""]
RUN dotnet restore "./snap7-to-prometheus.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "snap7-to-prometheus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "snap7-to-prometheus.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "snap7-to-prometheus.dll"]