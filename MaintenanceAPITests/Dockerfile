#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app
COPY "." .

RUN dotnet restore

ENTRYPOINT ["bash", "test.sh"]