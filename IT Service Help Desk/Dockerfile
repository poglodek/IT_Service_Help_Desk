#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IT Service Help Desk/IT Service Help Desk.csproj", "IT Service Help Desk/"]
RUN dotnet restore "IT Service Help Desk/IT Service Help Desk.csproj"
COPY . .
WORKDIR "/src/IT Service Help Desk"
RUN dotnet build "IT Service Help Desk.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IT Service Help Desk.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IT Service Help Desk.dll"]