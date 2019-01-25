FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

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
ARG MONGODB_USERNAME
ENV MONGODB_USERNAME=$MONGODB_USERNAME
ARG MONGODB_PASSWORD
ENV MONGODB_PASSWORD=$MONGODB_PASSWORD
ARG JWT_SECRET
ENV JWT_SECRET=$JWT_SECRET


COPY scripts_and_setup/configuration/ /app/configuration/

RUN echo "settings file: $APPLICATION_SETTINGS" && \
    rm /app/appsettings* && \
    cp /app/configuration/$APPLICATION_SETTINGS /app/appsettings.json

RUN sed -i "s|thedbusername|$MONGODB_USERNAME|g" /app/appsettings.json            
RUN sed -i "s|thedbpassword|$MONGODB_PASSWORD|g" /app/appsettings.json            
RUN sed -i "s|thisissplaceholderforthejwtsecret|$JWT_SECRET|g" /app/appsettings.json            

ENTRYPOINT ["dotnet", "NasGrad.API.dll"]
