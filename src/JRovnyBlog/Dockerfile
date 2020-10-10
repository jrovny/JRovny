FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM node:10.15-alpine as client
WORKDIR /app
COPY ClientApp/package*.json ./
RUN npm config set unsafe-perm true
RUN npm install
RUN npm install -g @angular/cli@9.0.0
COPY ClientApp ./
RUN npm run-script publish

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY JRovnyBlog.csproj .
RUN dotnet restore JRovnyBlog.csproj
COPY . .
#WORKDIR "/src/src/JRovnyBlog"
RUN dotnet build "JRovnyBlog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JRovnyBlog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
RUN mkdir dist
COPY --from=publish /app/publish .
COPY --from=client app/dist dist/
ENV ASPNETCORE_URLS http://*:5001
ENTRYPOINT ["dotnet", "JRovnyBlog.dll"]
