FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 39516
EXPOSE 44312

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["JwtToken/JwtToken.csproj", "JwtToken/"]
RUN dotnet restore "JwtToken/JwtToken.csproj"
COPY . .
WORKDIR "/src/JwtToken"
RUN dotnet build "JwtToken.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "JwtToken.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JwtToken.dll"]