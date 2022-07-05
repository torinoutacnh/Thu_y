#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Thu_y/Thu_y.csproj", "Thu_y/"]
RUN dotnet restore "Thu_y/Thu_y.csproj"

COPY . .
WORKDIR "/src/Thu_y"
RUN dotnet build "Thu_y.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Thu_y.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Thu_y.dll"]