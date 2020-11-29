FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.2-sdk-stretch AS build

WORKDIR /src
COPY src/MyGameIO.Api/MyGameIO.Api.csproj src/MyGameIO.Api/
COPY src/MyGameIO.Business/MyGameIO.Business.csproj src/MyGameIO.Business/
COPY src/MyGameIO.Data/MyGameIO.Data.csproj src/MyGameIO.Data/
RUN dotnet restore src/MyGameIO.Api/MyGameIO.Api.csproj
RUN dotnet restore src/MyGameIO.Business/MyGameIO.Business.csproj
RUN dotnet restore src/MyGameIO.Data/MyGameIO.Data.csproj
#
#WORKDIR /src
#COPY src/MyGameIO.Api/MyGameIO.Api.csproj src/MyGameIO.Api/
#COPY src/MyGameIO.Business/MyGameIO.Business.csproj src/MyGameIO.Business/
#COPY src/MyGameIO.Data/MyGameIO.Data.csproj src/MyGameIO.Data/
#RUN dotnet build src/MyGameIO.Api/MyGameIO.Api.csproj -c Release -o /app
#RUN dotnet build src/MyGameIO.Business/MyGameIO.Business.csproj -c Release -o /app
#RUN dotnet build src/MyGameIO.Data/MyGameIO.Data.csproj -c Release -o /app

COPY . . 
FROM build AS publish
RUN dotnet publish MyGameIO.Api/MyGameIO.Api.csproj --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT ["dotnet", "MyGamingControl.dll"]