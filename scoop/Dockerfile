FROM mcr.microsoft.com/powershell:nanoserver-1809

SHELL ["pwsh", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

COPY ./Robocopy.exe C:/Windows/System32/Robocopy.exe

RUN "iwr -useb get.scoop.sh | iex"