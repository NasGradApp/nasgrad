FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY NasGrad.API/NasGrad.API.csproj NasGrad.API/
COPY NasGrad.DBEngine/NasGrad.DBEngine.csproj NasGrad.DBEngine/
RUN dotnet restore NasGrad.API/NasGrad.API.csproj

COPY . ./

WORKDIR /src/NasGrad.API
RUN dotnet build NasGrad.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish NasGrad.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ARG APPLICATION_SETTINGS
ENV APPLICATION_SETTINGS=$APPLICATION_SETTINGS

COPY scripts_and_setup/configuration/ /app/configuration/

RUN echo "settings file: $APPLICATION_SETTINGS" && \
    rm /app/appsettings* && \
    cp /app/configuration/$APPLICATION_SETTINGS /app/appsettings.json

ENTRYPOINT ["dotnet", "NasGrad.API.dll"]
