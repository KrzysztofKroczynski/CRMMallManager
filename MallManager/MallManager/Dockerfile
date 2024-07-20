FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MallManager/MallManager/MallManager.csproj", "MallManager/MallManager/"]
COPY ["MallManager/MallManager.Client/MallManager.Client.csproj", "MallManager/MallManager.Client/"]
RUN dotnet restore "MallManager/MallManager/MallManager.csproj"
COPY . .
WORKDIR "/src/MallManager/MallManager"
RUN dotnet build "MallManager.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MallManager.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MallManager.dll"]
