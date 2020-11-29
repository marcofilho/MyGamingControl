FROM microsoft/dotnet:2.2-sdk as build

ARG BUILDCONFIG-RELEASE
ARG VERSION-1.0.0

COPY MyGameIO.Api.csproj /build/
COPY MyGameIO.Business.csproj /build
COPY MyGameIO.Data.csproj /build

RUN dotnet restore ./build/ MyGamingControl.sln

COPY . ./build/
WORKDIR /build
RUN dotnet publish ./MyGameIO.Api.csproj -c ${BUILDCONFIG-RELEASE} -o out /p:Version:$VERSION

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app

COPY --from=build /build/out .

ENTRYPOINT ["dotnet", "MyGamingControl.dll"]