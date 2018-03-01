FROM microsoft/aspnetcore-build:2.0

WORKDIR /code/app

COPY CzSharp.csproj .
COPY Directory.Build.props .
RUN dotnet restore


ENTRYPOINT dotnet watch --msbuildprojectextensionspath ../obj run --no-restore