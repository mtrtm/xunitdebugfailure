#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["XUnitTestProject1.Console/XUnitTestProject1.Console.csproj", "XUnitTestProject1.Console/"]
COPY ["XUnitTestProject1/XUnitTestProject1.csproj", "XUnitTestProject1/"]
RUN dotnet restore "XUnitTestProject1.Console/XUnitTestProject1.Console.csproj"
COPY . .
WORKDIR "/src/XUnitTestProject1.Console"
RUN dotnet build "XUnitTestProject1.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XUnitTestProject1.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XUnitTestProject1.Console.dll"]