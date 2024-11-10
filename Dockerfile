FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

WORKDIR /src

COPY ./GainsTracker.WebAPI/GainsTracker.WebAPI.csproj GainsTracker.WebAPI/

RUN dotnet restore GainsTracker.WebAPI/GainsTracker.WebAPI.csproj 

WORKDIR /src/GainsTracker.WebAPI
COPY . .

RUN dotnet build GainsTracker.WebAPI/GainsTracker.WebAPI.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish GainsTracker.WebAPI/GainsTracker.WebAPI.csproj -c Release -o /app/publish  --self-contained false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime

ENV ASPNETCORE_URLS=http://*:4040

WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 4040

ENTRYPOINT ["dotnet", "GainsTracker.WebAPI.dll"]