FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build

RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_8.x | bash - && \
    apt-get install -y build-essential nodejs

WORKDIR /src

ARG ADDRESS_OF_API
ENV ADDRESS_OF_API=$ADDRESS_OF_API

COPY NasGrad.Web/NasGrad.Web.csproj NasGrad.Web/
RUN dotnet restore NasGrad.Web/NasGrad.Web.csproj
COPY ./ ./
WORKDIR /src/NasGrad.Web

RUN rm ClientApp/src/apiConfig.js
RUn echo "export const apiHost=\""$ADDRESS_OF_API"\";" > ClientApp/src/apiConfig.js

RUN dotnet build NasGrad.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish NasGrad.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app ./

ENTRYPOINT ["dotnet", "NasGrad.Web.dll"]
