FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /source

COPY . .

RUN dotnet restore Api/TicketingSystem.FAQ/TicketingSystem.FAQ.csproj

RUN dotnet restore Core/FQA.InfraStructure/FAQ.InfraStructure.csproj

RUN dotnet restore Core/FQA.Application/FAQ.Application.csproj

RUN dotnet restore Core/FQA.Domain/FAQ.Domain.csproj

WORKDIR Api/TicketingSystem.FAQ

RUN dotnet publish TicketingSystem.FAQ.csproj -o /app/publish /p:UseAppHost=false

FROM build AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TicketingSystem.FAQ.dll"]