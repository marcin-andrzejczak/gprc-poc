#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GrpcPoc.Client/GrpcPoc.Client.csproj", "GrpcPoc.Client/"]
RUN dotnet restore "GrpcPoc.Client/GrpcPoc.Client.csproj"
COPY . .
WORKDIR "/src/GrpcPoc.Client"
RUN dotnet build "GrpcPoc.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcPoc.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcPoc.Client.dll"]