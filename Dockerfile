FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
COPY ./*.sln ./

WORKDIR /src

COPY */*.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .



#FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS base
#WORKDIR /app
#
#FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
#WORKDIR /src
#COPY ["MyGameIO.Api/MyGameIO.Api.csproj", "MyGameIO.Api/"]
#COPY ["MyGameIO.Business/MyGameIO.Business.csproj", "MyGameIO.Business/"]
#COPY ["MyGameIO.Api/MyGameIO.Api.csproj", "MyGameIO.Api/"]
#RUN dotnet restore "MyGameIO.Api/MyGameIO.Api.csproj"
#RUN dotnet restore "MyGameIO.Business/MyGameIO.Business.csproj"
#RUN dotnet restore "MyGameIO.Data/MyGameIO.Data.csproj"
#
#COPY . .
#WORKDIR "/src/MyGameIO.Api"
#RUN dotnet build "MyGameIO.Api.csproj" -c Release -o /app
#
#COPY . .
#WORKDIR "/src/MyGameIO.Business"
#RUN dotnet build "MyGameIO.Business.csproj" -c Release -o /app
#
#COPY . .
#WORKDIR "/src/MyGameIO.Data"
#RUN dotnet build "MyGameIO.Data.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "MyGameIO.Api.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .

ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT ["dotnet", "MyGamingControl.dll"]
