FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src

#COPY WAWEntity/WAWEntity.csproj WAWAPI/
#RUN dotnet restore WAWEntity/WAWEntity.csproj

COPY WAWAPI/WAWAPI.csproj WAWAPI/
COPY . .
RUN dotnet restore WAWAPI/WAWAPI.csproj

#RUN dotnet restore WAWEntity/WAWEntity.csproj

WORKDIR /src/WAWAPI
RUN dotnet build WAWAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WAWAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WAWAPI.dll"]
