FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OzonHelper/OzonHelper.csproj", "OzonHelper/"]
RUN dotnet restore "OzonHelper/OzonHelper.csproj"
COPY . .
WORKDIR "/src/OzonHelper"
RUN dotnet build "OzonHelper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OzonHelper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OzonHelper.dll"]
