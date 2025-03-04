FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
# RUN dotnet restore
# Build and publish a release
RUN dotnet tool install dotnet-sonarscanner --tool-path /App/.dotnet/tools
ENV PATH="$PATH:/App/.dotnet/tools"
# RUN chmod +x /App/.dotnet/tools/dotnet-sonarscanner
# RUN env
# RUN ls -ltra /App/.dotnet/tools/
# RUN cd /App
# RUN pwd
# RUN hostname
RUN dotnet sonarscanner begin /k:"mvc01" /d:sonar.host.url="http://192.168.11.114:9001"  /d:sonar.token="sqa_9b59fdfc44b3ebfdadbe945e3b0bbe7ae2943a59"
RUN dotnet publish -o out
RUN dotnet sonarscanner end /d:sonar.token="sqa_9b59fdfc44b3ebfdadbe945e3b0bbe7ae2943a59"

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:6c4df091e4e531bb93bdbfe7e7f0998e7ced344f54426b7e874116a3dc3233ff

ARG BUILD_NUMBER=0
ARG BUILD_TAG=local

LABEL version="3.0"
LABEL build_number=${BUILD_NUMBER}
LABEL build_tag=${BUILD_TAG}

WORKDIR /App
COPY --from=build /App/out .
ENTRYPOINT ["dotnet", "mvc01.dll"]
