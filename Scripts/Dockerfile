FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

ARG BUILD_CONFIG=Release

# Copy the solution and restore dependencies
COPY EmployeeService/EmployeeService.Api/EmployeeService.Api.csproj ./EmployeeService.Api/
COPY EmployeeService/EmployeeService.Application/EmployeeService.Application.csproj ./EmployeeService.Application/
COPY EmployeeService/EmployeeService.DataAccess/EmployeeService.DataAccess.csproj ./EmployeeService.DataAccess/
COPY EmployeeService/EmployeeService.Domain/EmployeeService.Domain.csproj ./EmployeeService.Domain/
COPY EmployeeService/EmployeeService.sln .
RUN dotnet restore ./EmployeeService.sln

# Copy the rest of the code and build
COPY EmployeeService/EmployeeService.Api/ ./EmployeeService.Api/
COPY EmployeeService/EmployeeService.Application/ ./EmployeeService.Application/
COPY EmployeeService/EmployeeService.DataAccess/ ./EmployeeService.DataAccess/
COPY EmployeeService/EmployeeService.Domain/ ./EmployeeService.Domain/
WORKDIR /app/EmployeeService.Api
RUN dotnet publish -c $BUILD_CONFIG -o out

# Use the runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/EmployeeService.Api/out ./
ENTRYPOINT ["dotnet", "EmployeeService.Api.dll"]