FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 5000


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS test
WORKDIR /app
# COPY PROJECT
COPY Sample.sln .
COPY src/Client/Sample.Client.csproj src/Client/
COPY src/Handler/Sample.Handler.csproj src/Handler/
COPY src/Processor/Sample.Processor.csproj src/Processor/
COPY src/Domain/Sample.Domain.csproj src/Domain/
# COPY TEST
COPY test/Client.Test/Sample.Client.Test.csproj test/Client.Test/
COPY test/Handler.Test/Sample.Handler.Test.csproj test/Handler.Test/
COPY test/Processor.Test/Sample.Processor.Test.csproj test/Processor.Test/
COPY test/Domain.Test/Sample.Domain.Test.csproj test/Domain.Test/
# RESTORE PROJECTS
RUN dotnet restore

# COPY OTHER FILES INTO PROJECT
COPY src/Client/ src/Client/
COPY src/Handler/ src/Handler/
COPY src/Processor/ src/Processor/
COPY src/Domain/ src/Domain/

COPY test/Client.Test/ test/Client.Test/
COPY test/Handler.Test/ test/Handler.Test/
COPY test/Processor.Test/ test/Processor.Test/
COPY test/Domain.Test/ test/Domain.Test/

# RUN TEST
RUN dotnet test


FROM test AS build
WORKDIR /src

COPY --from=test /app/src/Client/ Client/
COPY --from=test /app/src/Handler/ Handler/
COPY --from=test /app/src/Processor/ Processor/
COPY --from=test /app/src/Domain/ Domain/


FROM build AS publish
RUN dotnet publish /src/Client/Sample.Client.csproj -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Client.dll"]