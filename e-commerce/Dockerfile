FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["e-commerce/e-commerce.csproj", "e-commerce/"]
RUN dotnet restore "e-commerce/e-commerce.csproj"
COPY . .
WORKDIR "/src/e-commerce"
RUN dotnet build "e-commerce.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "e-commerce.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "e-commerce.dll"]
