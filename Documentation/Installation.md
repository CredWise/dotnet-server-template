# Installation

## <a name="software"></a>Software

The following software are require to starting using this server:

- [.Net 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Docker](https://www.docker.com/products/docker-desktop)

## <a name="setup"></a>Setup

Create `appsettings.json` in `src/Client`. Copy the content in `appsettings.json.dist` into `appsettings.json` and set the appropriate values.

Note: do **NOT** add production values to `appsettings.json.dist`. It will be added to git history, moreover do **NOT** delete `appsettings.json.dist`.

## <a name="docker"></a>Docker Image

Run `./build.sh` to build the docker image on your local machine or run `docker pull ghcr.io/credwise/dotnet-server-template:main` for the latest official release
