FROM mcr.microsoft.com/powershell:nanoserver-1809

USER ContainerAdministrator

ENV SCOOP "C:\scoop"
ENV SCOOP_HOME "C:\scoop\apps\scoop\current"

SHELL ["pwsh", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

COPY ./Robocopy.exe C:/Windows/System32/Robocopy.exe

RUN iwr -useb get.scoop.sh | iex; setx /M PATH $($env:PATH + ';' + $env:SCOOP_HOME);