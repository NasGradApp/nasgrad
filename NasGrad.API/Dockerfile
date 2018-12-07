FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["NasGrad.API.csproj", "NasGrad.API/"]
RUN dotnet restore "NasGrad.API/NasGrad.API.csproj"

COPY ./ ./NasGrad.API
WORKDIR "/src/NasGrad.API"
RUN dotnet build "NasGrad.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NasGrad.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NasGrad.API.dll"]