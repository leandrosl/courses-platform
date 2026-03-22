FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/CoursesPlatform.Api/CoursesPlatform.Api.csproj src/CoursesPlatform.Api/
RUN dotnet restore src/CoursesPlatform.Api/CoursesPlatform.Api.csproj

COPY src/ src/
WORKDIR /src/src/CoursesPlatform.Api
RUN dotnet publish CoursesPlatform.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CoursesPlatform.Api.dll"]
