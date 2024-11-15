FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY . .
RUN dotnet restore "MasterTagSystem/MasterTagSystem.csproj"
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5000
ENTRYPOINT ["dotnet", "run", "--project", "MasterTagSystem/MasterTagSystem.csproj"]