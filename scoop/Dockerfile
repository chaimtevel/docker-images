FROM mcr.microsoft.com/powershell:nanoserver-1903

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# USER ContainerAdministrator

COPY ./Robocopy.exe C:/Windows/System32/Robocopy.exe

RUN iwr -useb get.scoop.sh | iex
