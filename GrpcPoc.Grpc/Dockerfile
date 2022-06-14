#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GrpcPoc.Grpc/GrpcPoc.Grpc.csproj", "GrpcPoc.Grpc/"]
RUN dotnet restore "GrpcPoc.Grpc/GrpcPoc.Grpc.csproj"
COPY . .
WORKDIR "/src/GrpcPoc.Grpc"
RUN dotnet build "GrpcPoc.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcPoc.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcPoc.Grpc.dll"]