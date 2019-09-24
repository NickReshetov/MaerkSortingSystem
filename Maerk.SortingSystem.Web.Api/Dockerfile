FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Maerk.SortingSystem.Web.Api/Maerk.SortingSystem.Web.Api.csproj", "Maerk.SortingSystem.Web.Api/"]
COPY ["Maerk.SortingSystem.Services/Maerk.SortingSystem.Services.csproj", "Maerk.SortingSystem.Services/"]
COPY ["Maerk.SortingSystem.DataAccess/Maerk.SortingSystem.DataAccess.csproj", "Maerk.SortingSystem.DataAccess/"]
COPY ["Maerk.SortingSystem.Dtos/Maerk.SortingSystem.Dtos.csproj", "Maerk.SortingSystem.Dtos/"]
COPY ["Maerk.SortingSystem.Common/Maerk.SortingSystem.Common.csproj", "Maerk.SortingSystem.Common/"]
COPY ["Maerk.SortingSystem.Worker/Maerk.SortingSystem.Worker.csproj", "Maerk.SortingSystem.Worker/"]
RUN dotnet restore "Maerk.SortingSystem.Web.Api/Maerk.SortingSystem.Web.Api.csproj"
COPY . .
WORKDIR "/src/Maerk.SortingSystem.Web.Api"
RUN dotnet build "Maerk.SortingSystem.Web.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Maerk.SortingSystem.Web.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Maerk.SortingSystem.Web.Api.dll"]