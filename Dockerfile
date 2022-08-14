FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=Development
#ENV ASPNETCORE_URLS=http://+:80 <-- commented for Heroku
#ENV ASPNETCORE_URLS=http://+:80;https://+:443 <-- unable to user https yet

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY src .
RUN dotnet restore "WheelsAndGoods.Api/WheelsAndGoods.Api.csproj"
RUN dotnet build "WheelsAndGoods.Api/WheelsAndGoods.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WheelsAndGoods.Api/WheelsAndGoods.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish  /app/publish .
CMD dotnet dev-certs https --clean
CMD dotnet dev-certs https
#CMD dotnet WheelsAndGoods.Api.dll <-- commented for Heroku
CMD ASPNETCORE_URLS=http://*:$PORT dotnet WheelsAndGoods.Api.dll