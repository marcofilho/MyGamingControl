FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["MyGameIO.Api/MyGameIO.Api.csproj", "MyGameIO.Api/"]
COPY ["MyGameIO.Business/MyGameIO.Business.csproj", "MyGameIO.Business/"]
COPY ["MyGameIO.Data/MyGameIO.Data.csproj", "MyGameIO.Data/"]
RUN dotnet restore "MyGameIO.Api/MyGameIO.Api.csproj"
COPY . .
WORKDIR "/src/MyGameIO.Api"
RUN dotnet build "MyGameIO.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MyGameIO.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyGameIO.Api.dll"]