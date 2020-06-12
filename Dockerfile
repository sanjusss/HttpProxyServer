#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HttpProxyServer.csproj", ""]
RUN dotnet restore "./HttpProxyServer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HttpProxyServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HttpProxyServer.csproj" -c Release -o /app/publish

FROM base AS final
MAINTAINER sanjusss@qq.com
ENV USER=""
ENV PASSWORD=""
ENV ENDPOINTS="0.0.0.0:8000"
EXPOSE 8000
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HttpProxyServer.dll"]