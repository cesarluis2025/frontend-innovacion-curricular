FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . .

EXPOSE 8081

CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:8081", "--no-launch-profile"]
