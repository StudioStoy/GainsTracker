FROM mcr.microsoft.com/dotnet/sdk:7.0 as build

WORKDIR /src

COPY ./GainsTracker.CoreAPI/GainsTracker.CoreAPI.csproj GainsTracker.CoreAPI/

RUN dotnet restore GainsTracker.CoreAPI/GainsTracker.CoreAPI.csproj 

WORKDIR /src/GainsTracker.CoreAPI
COPY . .

RUN dotnet build GainsTracker.CoreAPI/GainsTracker.CoreAPI.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish GainsTracker.CoreAPI/GainsTracker.CoreAPI.csproj -c Release -o /app/publish  --self-contained false

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime

ENV ASPNETCORE_URLS=http://*:4040

WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 4040

ENTRYPOINT ["dotnet", "GainsTracker.CoreAPI.dll"]