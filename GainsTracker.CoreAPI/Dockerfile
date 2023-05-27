FROM mcr.microsoft.com/dotnet/aspnet:7.0

ENV ASPNETCORE_URLS=http://*:4040
ENV ASPNETCORE_ENVIRONMENT=Development

COPY ./bin/Release/net7.0/publish App/
WORKDIR /App
EXPOSE 4040

ENTRYPOINT ["dotnet","GainsTracker.CoreAPI.dll"]