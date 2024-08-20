FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app 
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["DownloadFile.csproj", "./"]
RUN dotnet restore "DownloadFile.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DownloadFile.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "DownloadFile.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "DownloadFile.dll"]