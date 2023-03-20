#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Car.Rental.UI/Car.Rental.UI.csproj", "Car.Rental.UI/"]
COPY ["Authorization.API/Authorization.API.csproj", "Authorization.API/"]
COPY ["Authorization/Authorization.csproj", "Authorization/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["User/User.csproj", "User/"]
COPY ["Car/Car.csproj", "Car/"]
COPY ["Rent/Rent.csproj", "Rent/"]
COPY ["Mapping/Mapping.csproj", "Mapping/"]
COPY ["Car.API/Car.API.csproj", "Car.API/"]
COPY ["Rent.API/Rent.API.csproj", "Rent.API/"]
COPY ["User.API/User.API.csproj", "User.API/"]
RUN dotnet restore "Car.Rental.UI/Car.Rental.UI.csproj"
COPY . .
WORKDIR "/src/Car.Rental.UI"
RUN dotnet build "Car.Rental.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Car.Rental.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Car.Rental.UI.dll"]