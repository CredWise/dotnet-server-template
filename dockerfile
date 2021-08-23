FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore

FROM build AS test
RUN dotnet test

FROM build AS publish
RUN dotnet publish src/Presentation/Presentation.csproj -c Release -o /app/publish -f net5.0

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]