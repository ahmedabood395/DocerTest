#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5171
EXPOSE 7229

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/Services/Api/TicketingSystem.FAQ/TicketingSystem.FAQ.csproj", "Src/Services/Api/TicketingSystem.FAQ/"]
COPY ["Src/Services/Core/FQA.InfraStructure/FAQ.InfraStructure.csproj", "Src/Services/Core/FQA.InfraStructure/"]
COPY ["Src/Services/Core/FQA.Domain/FAQ.Domain.csproj", "Src/Services/Core/FQA.Domain/"]
COPY ["Src/Services/Core/FQA.Application/FAQ.Application.csproj", "Src/Services/Core/FQA.Application/"]
RUN dotnet restore "./Src/Services/Api/TicketingSystem.FAQ/./TicketingSystem.FAQ.csproj"
COPY . .
WORKDIR "/src/Src/Services/Api/TicketingSystem.FAQ"
RUN dotnet build "./TicketingSystem.FAQ.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TicketingSystem.FAQ.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketingSystem.FAQ.dll"]