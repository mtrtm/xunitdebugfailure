#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["XUnitTestProject1/XUnitTestProject1.csproj", "XUnitTestProject1/"]
RUN dotnet restore "XUnitTestProject1/XUnitTestProject1.csproj"
COPY . .
WORKDIR "/src/XUnitTestProject1"
RUN dotnet build "XUnitTestProject1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XUnitTestProject1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XUnitTestProject1.dll"]
