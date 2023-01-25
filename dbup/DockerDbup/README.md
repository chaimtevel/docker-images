This image runs sql scripts using [Dbup](https://dbup.github.io/). For now we only built for SQL Server, but support can be added for additional database engines.

### Command Line Args

Here are the currently supported commands:

1. migrate
1. info

These options are globally available for these commands:

__Connection String__

```sh
-c "some connection string"
--connection-string "some connection string"
```

Alternatively, you can just set an environment variable named `CONNECTION_STRING`.

__Scripts Path__

By default the runner looks for scripts at `/dbup/sql`

```sh
-p "some path"
--scripts-path "some path"
```

Alternatively, you can just set an environment variable named `SCRIPTS_PATH`.

### Idempotent

Any file that has that term in its name will be considered idempotent and be run every time.

### Build

#### Single file build

```sh
dotnet publish -r win-x64 -c release --sc true -o .\bin\win -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:PublishTrimmed=true

# or

 dotnet publish -r linux-musl-x64  -c release --sc true -o .\bin\linux -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:PublishTrimmed=true

```

#### Docker build

```sh
 docker build -t git.bitbean.com:4567/internal/docker-images/dbup:1.0.5  .
```