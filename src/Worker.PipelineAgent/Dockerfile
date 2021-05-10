FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Worker.PipelineAgent/Worker.PipelineAgent.csproj", "src/Worker.PipelineAgent/"]
COPY ["src/Common.Models/Common.Models.csproj", "src/Common.Models/"]
RUN dotnet restore "src/Worker.PipelineAgent/Worker.PipelineAgent.csproj"
COPY . .
WORKDIR "/src/src/Worker.PipelineAgent"
RUN dotnet build "Worker.PipelineAgent.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Worker.PipelineAgent.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Worker.PipelineAgent.dll"]