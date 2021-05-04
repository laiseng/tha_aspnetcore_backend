#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["tha_api/tha_api.csproj", "tha_api/"]
RUN dotnet restore "tha_api/tha_api.csproj"
COPY . .
WORKDIR "/src/tha_api"
RUN dotnet build "tha_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "tha_api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "tha_api.dll"]