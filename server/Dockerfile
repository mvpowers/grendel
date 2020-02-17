FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# copy csproj and restore as distinct layers
WORKDIR /src
COPY *.sln .
COPY GrendelApi/*.csproj ./GrendelApi/
COPY GrendelCommon/*.csproj ./GrendelCommon/
COPY GrendelData/*.csproj ./GrendelData/

RUN dotnet restore

# copy everything else and build app
COPY . .

WORKDIR /src/GrendelApi
RUN dotnet publish -c Release -o /app/GrendelApi

WORKDIR /src/GrendelCommon
RUN dotnet publish -c Release -o /app/GrendelCommon

WORKDIR /src/GrendelData
RUN dotnet publish -c Release -o /app/GrendelData

FROM base AS final
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "GrendelApi/GrendelApi.dll"]
