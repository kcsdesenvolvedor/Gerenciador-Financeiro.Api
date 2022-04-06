#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Gerenciador-Financeiro.API/Gerenciador-Financeiro.API.csproj", "Gerenciador-Financeiro.API/"]
RUN dotnet restore "Gerenciador-Financeiro.API/Gerenciador-Financeiro.API.csproj"
COPY *.csproj ./
RUN dotnet restore

COPY . ./
WORKDIR "/src/Gerenciador-Financeiro.API"
RUN dotnet build "Gerenciador-Financeiro.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gerenciador-Financeiro.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Gerenciador-Financeiro.API.dll
#ENTRYPOINT ["dotnet", "Gerenciador-Financeiro.API.dll"]

