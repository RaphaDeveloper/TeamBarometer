FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY src/API/API.csproj ./API/
COPY src/Domain/Domain.csproj ./Domain/
COPY src/Application/Application.csproj ./Application/
RUN dotnet restore ./API/API.csproj
COPY ./src .
WORKDIR /src/API
RUN dotnet build API.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "API.dll"]