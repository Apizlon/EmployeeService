using EmployeeService.Application.Contracts;
using EmployeeService.Application.Contracts.Employee;
using EmployeeService.Application.Interfaces.Repositories;
using EmployeeService.Application.Interfaces.Services;
using EmployeeService.Application.Interfaces.UnitOfWork;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Exceptions;
using Moq;

namespace EmployeeService.Tests.Services;

public class EmployeeServiceTests
{
    private readonly Mock<IUnitOfWorkFactory> _unitOfWorkFactoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<IPassportRepository> _passportRepositoryMock;
    private readonly IEmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _departmentRepositoryMock = new Mock<IDepartmentRepository>();
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _passportRepositoryMock = new Mock<IPassportRepository>();

        _unitOfWorkFactoryMock.Setup(f => f.Create(OnDispose.Commit)).Returns(_unitOfWorkMock.Object);
        _unitOfWorkMock.Setup(u => u.GetRepository<IDepartmentRepository>()).Returns(_departmentRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.GetRepository<IEmployeeRepository>()).Returns(_employeeRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.GetRepository<IPassportRepository>()).Returns(_passportRepositoryMock.Object);

        _employeeService = new Application.Services.EmployeeService(_unitOfWorkFactoryMock.Object);
    }

    [Fact]
    public async Task AddEmployeeAsync_WithValidRequest_ShouldReturnId()
    {
        // Arrange
        int expectedId = 1;
        int passportId = 2;
        var employeeRequest = new AddEmployeeRequest
        {
            Name = "John",
            Surname = "Doe",
            Phone = "+1234567890",
            CompanyId = 1,
            DepartmentId = 1,
            Passport = new PassportDto { Type = "Passport", Number = "1234567890" }
        };
        var department = new Department { Id = 1, CompanyId = 1, Name = "Test Department", Phone = "+1234567890" };
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId, default)).ReturnsAsync(department);
        _passportRepositoryMock.Setup(repo => repo.AddPassportAsync(It.IsAny<Passport>())).ReturnsAsync(passportId);
        _employeeRepositoryMock.Setup(repo => repo.AddEmployeeAsync(It.IsAny<Employee>())).ReturnsAsync(expectedId);

        // Act
        var result = await _employeeService.AddEmployeeAsync(employeeRequest);

        // Assert
        Assert.Equal(expectedId, result);
        _unitOfWorkMock.Verify(u => u.BeginTransaction(), Times.Once);
        _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId, default), Times.Once);
        _passportRepositoryMock.Verify(repo => repo.AddPassportAsync(It.IsAny<Passport>()), Times.Once);
        _employeeRepositoryMock.Verify(repo => repo.AddEmployeeAsync(It.IsAny<Employee>()), Times.Once);
    }

    [Fact]
    public async Task AddEmployeeAsync_WithInvalidDepartmentId_ShouldThrowDepartmentNotFoundException()
    {
        // Arrange
        var employeeRequest = new AddEmployeeRequest
        {
            Name = "John",
            Surname = "Doe",
            Phone = "+1234567890",
            CompanyId = 1,
            DepartmentId = 1,
            Passport = new PassportDto { Type = "Passport", Number = "1234567890" }
        };
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId, default)).ReturnsAsync((Department?)null);

        // Act
        var func = async () => await _employeeService.AddEmployeeAsync(employeeRequest);

        // Assert
        await Assert.ThrowsAsync<DepartmentNotFoundException>(func);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId, default), Times.Once);
    }

    [Fact]
    public async Task AddEmployeeAsync_WithMismatchedCompanyAndDepartment_ShouldThrowBadRequestException()
    {
        // Arrange
        var employeeRequest = new AddEmployeeRequest
        {
            Name = "John",
            Surname = "Doe",
            Phone = "+1234567890",
            CompanyId = 1,
            DepartmentId = 1,
            Passport = new PassportDto { Type = "Passport", Number = "1234567890" }
        };
        var department = new Department { Id = 1, CompanyId = 2, Name = "Test Department", Phone = "+1234567890" };
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId, default)).ReturnsAsync(department);

        // Act
        var func = async () => await _employeeService.AddEmployeeAsync(employeeRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId, default), Times.Once);
    }

    [Fact]
    public async Task AddEmployeeAsync_WithEmptyName_ShouldThrowBadRequestException()
    {
        // Arrange
        var employeeRequest = new AddEmployeeRequest
        {
            Name = "",
            Surname = "Doe",
            Phone = "+1234567890",
            CompanyId = 1,
            DepartmentId = 1,
            Passport = new PassportDto { Type = "Passport", Number = "1234567890" }
        };
        var department = new Department { Id = 1, CompanyId = 1, Name = "Test Department", Phone = "+1234567890" };
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId, default)).ReturnsAsync(department);

        // Act
        var func = async () => await _employeeService.AddEmployeeAsync(employeeRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId, default), Times.Once);
    }

    [Fact]
    public async Task AddEmployeeAsync_WithInvalidPhone_ShouldThrowBadRequestException()
    {
        // Arrange
        var employeeRequest = new AddEmployeeRequest
        {
            Name = "John",
            Surname = "Doe",
            Phone = "invalid-phone",
            CompanyId = 1,
            DepartmentId = 1,
            Passport = new PassportDto { Type = "Passport", Number = "1234567890" }
        };
        var department = new Department { Id = 1, CompanyId = 1, Name = "Test Department", Phone = "+1234567890" };
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId, default)).ReturnsAsync(department);

        // Act
        var func = async () => await _employeeService.AddEmployeeAsync(employeeRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId, default), Times.Once);
    }

    [Fact]
    public async Task AddEmployeeAsync_WithInvalidPassportNumber_ShouldThrowBadRequestException()
    {
        // Arrange
        var employeeRequest = new AddEmployeeRequest
        {
            Name = "John",
            Surname = "Doe",
            Phone = "+1234567890",
            CompanyId = 1,
            DepartmentId = 1,
            Passport = new PassportDto { Type = "Passport", Number = "invalid-number" }
        };
        var department = new Department { Id = 1, CompanyId = 1, Name = "Test Department", Phone = "+1234567890" };
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId, default)).ReturnsAsync(department);

        // Act
        var func = async () => await _employeeService.AddEmployeeAsync(employeeRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId, default), Times.Once);
    }

    [Fact]
    public async Task GetEmployeeAsync_WithValidId_ShouldReturnEmployeeResponse()
    {
        // Arrange
        int id = 1;
        var employee = new Employee { Id = id, Name = "John", Surname = "Doe", Phone = "+1234567890", CompanyId = 1, DepartmentId = 1, PassportId = 2 };
        var passport = new Passport { Id = 2, Type = "Passport", Number = "1234567890" };
        var department = new Department { Id = 1, CompanyId = 1, Name = "Test Department", Phone = "+1234567890" };
        _employeeRepositoryMock.Setup(repo => repo.GetEmployeeAsync(id, default)).ReturnsAsync(employee);
        _passportRepositoryMock.Setup(repo => repo.GetPassportAsync(employee.PassportId, default)).ReturnsAsync(passport);
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(employee.DepartmentId, default)).ReturnsAsync(department);

        // Act
        var result = await _employeeService.GetEmployeeAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(employee.Id, result.Id);
        Assert.Equal(employee.Name, result.Name);
        Assert.Equal(employee.Surname, result.Surname);
        Assert.Equal(employee.Phone, result.Phone);
        Assert.Equal(employee.CompanyId, result.CompanyId);
        Assert.Equal(passport.Type, result.Passport!.Type);
        Assert.Equal(passport.Number, result.Passport.Number);
        Assert.Equal(department.Name, result.Department!.Name);
        Assert.Equal(department.Phone, result.Department.Phone);
        _employeeRepositoryMock.Verify(repo => repo.GetEmployeeAsync(id, default), Times.Once);
        _passportRepositoryMock.Verify(repo => repo.GetPassportAsync(employee.PassportId, default), Times.Once);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(employee.DepartmentId, default), Times.Once);
    }

    [Fact]
    public async Task GetEmployeeAsync_WithInvalidId_ShouldThrowEmployeeNotFoundException()
    {
        // Arrange
        int id = 1;
        _employeeRepositoryMock.Setup(repo => repo.GetEmployeeAsync(id, default)).ReturnsAsync((Employee?)null);

        // Act
        var func = async () => await _employeeService.GetEmployeeAsync(id);

        // Assert
        await Assert.ThrowsAsync<EmployeeNotFoundException>(func);
        _employeeRepositoryMock.Verify(repo => repo.GetEmployeeAsync(id, default), Times.Once);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_WithValidId_ShouldSucceed()
    {
        // Arrange
        int id = 1;
        var employee = new Employee { Id = id, Name = "John", Surname = "Doe", Phone = "+1234567890", CompanyId = 1, DepartmentId = 1, PassportId = 2 };
        _employeeRepositoryMock.Setup(repo => repo.GetEmployeeAsync(id, default)).ReturnsAsync(employee);

        // Act
        await _employeeService.DeleteEmployeeAsync(id);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransaction(), Times.Once);
        _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        _employeeRepositoryMock.Verify(repo => repo.GetEmployeeAsync(id, default), Times.Once);
        _employeeRepositoryMock.Verify(repo => repo.DeleteEmployeeAsync(id), Times.Once);
        _passportRepositoryMock.Verify(repo => repo.DeletePassportAsync(employee.PassportId), Times.Once);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_WithInvalidId_ShouldThrowEmployeeNotFoundException()
    {
        // Arrange
        int id = 1;
        _employeeRepositoryMock.Setup(repo => repo.GetEmployeeAsync(id, default)).ReturnsAsync((Employee?)null);

        // Act
        var func = async () => await _employeeService.DeleteEmployeeAsync(id);

        // Assert
        await Assert.ThrowsAsync<EmployeeNotFoundException>(func);
        _employeeRepositoryMock.Verify(repo => repo.GetEmployeeAsync(id, default), Times.Once);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_WithValidRequest_ShouldSucceed()
    {
        // Arrange
        int id = 1;
        var employeeRequest = new UpdateEmployeeRequest
        {
            Name = "John",
            Surname = "Doe",
            Phone = "+1234567890",
            CompanyId = 1,
            DepartmentId = 1,
            Passport = new PassportDto { Type = "Passport", Number = "1234567890" }
        };
        var oldEmployee = new Employee { Id = id, Name = "Old", Surname = "Employee", Phone = "+0987654321", CompanyId = 1, DepartmentId = 1, PassportId = 2 };
        var department = new Department { Id = 1, CompanyId = 1, Name = "Test Department", Phone = "+1234567890" };
        _employeeRepositoryMock.Setup(repo => repo.GetEmployeeAsync(id, default)).ReturnsAsync(oldEmployee);
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId!.Value, default)).ReturnsAsync(department);

        // Act
        await _employeeService.UpdateEmployeeAsync(id, employeeRequest);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransaction(), Times.Once);
        _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        _employeeRepositoryMock.Verify(repo => repo.GetEmployeeAsync(id, default), Times.Once);
        _employeeRepositoryMock.Verify(repo => repo.UpdateEmployeeAsync(id, It.IsAny<Employee>()), Times.Once);
        _passportRepositoryMock.Verify(repo => repo.UpdatePassportAsync(It.IsAny<Passport>()), Times.Once);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId!.Value, default), Times.Once);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_WithInvalidId_ShouldThrowEmployeeNotFoundException()
    {
        // Arrange
        int id = 1;
        var employeeRequest = new UpdateEmployeeRequest
        {
            Name = "John",
            Surname = "Doe",
            Phone = "+1234567890",
            CompanyId = 1,
            DepartmentId = 1
        };
        _employeeRepositoryMock.Setup(repo => repo.GetEmployeeAsync(id, default)).ReturnsAsync((Employee?)null);

        // Act
        var func = async () => await _employeeService.UpdateEmployeeAsync(id, employeeRequest);

        // Assert
        await Assert.ThrowsAsync<EmployeeNotFoundException>(func);
        _employeeRepositoryMock.Verify(repo => repo.GetEmployeeAsync(id, default), Times.Once);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_WithInvalidDepartmentId_ShouldThrowDepartmentNotFoundException()
    {
        // Arrange
        int id = 1;
        var employeeRequest = new UpdateEmployeeRequest
        {
            Name = "John",
            Surname = "Doe",
            Phone = "+1234567890",
            CompanyId = 1,
            DepartmentId = 1
        };
        var oldEmployee = new Employee { Id = id, Name = "Old", Surname = "Employee", Phone = "+0987654321", CompanyId = 1, DepartmentId = 1, PassportId = 2 };
        _employeeRepositoryMock.Setup(repo => repo.GetEmployeeAsync(id, default)).ReturnsAsync(oldEmployee);
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId!.Value, default)).ReturnsAsync((Department?)null);

        // Act
        var func = async () => await _employeeService.UpdateEmployeeAsync(id, employeeRequest);

        // Assert
        await Assert.ThrowsAsync<DepartmentNotFoundException>(func);
        _employeeRepositoryMock.Verify(repo => repo.GetEmployeeAsync(id, default), Times.Once);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId!.Value, default), Times.Once);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_WithMismatchedCompanyAndDepartment_ShouldThrowBadRequestException()
    {
        // Arrange
        int id = 1;
        var employeeRequest = new UpdateEmployeeRequest
        {
            Name = "John",
            Surname = "Doe",
            Phone = "+1234567890",
            CompanyId = 1,
            DepartmentId = 1
        };
        var oldEmployee = new Employee { Id = id, Name = "Old", Surname = "Employee", Phone = "+0987654321", CompanyId = 1, DepartmentId = 1, PassportId = 2 };
        var department = new Department { Id = 1, CompanyId = 2, Name = "Test Department", Phone = "+1234567890" };
        _employeeRepositoryMock.Setup(repo => repo.GetEmployeeAsync(id, default)).ReturnsAsync(oldEmployee);
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId!.Value, default)).ReturnsAsync(department);

        // Act
        var func = async () => await _employeeService.UpdateEmployeeAsync(id, employeeRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _employeeRepositoryMock.Verify(repo => repo.GetEmployeeAsync(id, default), Times.Once);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId!.Value, default), Times.Once);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_WithInvalidPassportNumber_ShouldThrowBadRequestException()
    {
        // Arrange
        int id = 1;
        var employeeRequest = new UpdateEmployeeRequest
        {
            Name = "John",
            Surname = "Doe",
            Phone = "+1234567890",
            CompanyId = 1,
            DepartmentId = 1,
            Passport = new PassportDto { Type = "Passport", Number = "invalid-number" }
        };
        var oldEmployee = new Employee { Id = id, Name = "Old", Surname = "Employee", Phone = "+0987654321", CompanyId = 1, DepartmentId = 1, PassportId = 2 };
        var department = new Department { Id = 1, CompanyId = 1, Name = "Test Department", Phone = "+1234567890" };
        _employeeRepositoryMock.Setup(repo => repo.GetEmployeeAsync(id, default)).ReturnsAsync(oldEmployee);
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId!.Value, default)).ReturnsAsync(department);

        // Act
        var func = async () => await _employeeService.UpdateEmployeeAsync(id, employeeRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _employeeRepositoryMock.Verify(repo => repo.GetEmployeeAsync(id, default), Times.Once);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(employeeRequest.DepartmentId!.Value, default), Times.Once);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_WithValidData_ShouldReturnList()
    {
        // Arrange
        var employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John", Surname = "Doe", Phone = "+1234567890", CompanyId = 1, DepartmentId = 1, PassportId = 2 },
            new Employee { Id = 2, Name = "Jane", Surname = "Smith", Phone = "+0987654321", CompanyId = 1, DepartmentId = 1, PassportId = 3 }
        };
        var passports = new Dictionary<int, Passport>
        {
            { 2, new Passport { Id = 2, Type = "Passport", Number = "1234567890" } },
            { 3, new Passport { Id = 3, Type = "Passport", Number = "0987654321" } }
        };
        var department = new Department { Id = 1, CompanyId = 1, Name = "Test Department", Phone = "+1234567890" };
        _employeeRepositoryMock.Setup(repo => repo.GetAllEmployeesAsync(default)).ReturnsAsync(employees);
        _passportRepositoryMock.Setup(repo => repo.GetPassportAsync(It.IsAny<int>(), default))
            .Returns<int, CancellationToken>((id, _) => Task.FromResult(passports[id])!);
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(It.IsAny<int>(), default)).ReturnsAsync(department);

        // Act
        var result = await _employeeService.GetAllEmployeesAsync();

        // Assert
        Assert.Equal(employees.Count, result.Count);
        Assert.All(result, r => Assert.Equal(department.Name, r.Department.Name));
        _employeeRepositoryMock.Verify(repo => repo.GetAllEmployeesAsync(default), Times.Once);
        _passportRepositoryMock.Verify(repo => repo.GetPassportAsync(It.IsAny<int>(), default), Times.Exactly(employees.Count));
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(It.IsAny<int>(), default), Times.Exactly(employees.Count));
    }

    [Fact]
    public async Task GetEmployeesByCompanyIdAsync_WithValidCompanyId_ShouldReturnList()
    {
        // Arrange
        int companyId = 1;
        var employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John", Surname = "Doe", Phone = "+1234567890", CompanyId = companyId, DepartmentId = 1, PassportId = 2 }
        };
        var passport = new Passport { Id = 2, Type = "Passport", Number = "1234567890" };
        var department = new Department { Id = 1, CompanyId = companyId, Name = "Test Department", Phone = "+1234567890" };
        _employeeRepositoryMock.Setup(repo => repo.GetEmployeesByCompanyIdAsync(companyId, default)).ReturnsAsync(employees);
        _passportRepositoryMock.Setup(repo => repo.GetPassportAsync(It.IsAny<int>(), default)).ReturnsAsync(passport);
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(It.IsAny<int>(), default)).ReturnsAsync(department);

        // Act
        var result = await _employeeService.GetEmployeesByCompanyIdAsync(companyId);

        // Assert
        Assert.Single(result);
        Assert.Equal(employees[0].Name, result[0].Name);
        Assert.Equal(passport.Type, result[0].Passport!.Type);
        Assert.Equal(department.Name, result[0].Department!.Name);
        _employeeRepositoryMock.Verify(repo => repo.GetEmployeesByCompanyIdAsync(companyId, default), Times.Once);
        _passportRepositoryMock.Verify(repo => repo.GetPassportAsync(It.IsAny<int>(), default), Times.Once);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(It.IsAny<int>(), default), Times.Once);
    }

    [Fact]
    public async Task GetEmployeesByDepartmentIdAsync_WithValidDepartmentId_ShouldReturnList()
    {
        // Arrange
        int departmentId = 1;
        var employees = new List<Employee>
        {
            new() { Id = 1, Name = "John", Surname = "Doe", Phone = "+1234567890", CompanyId = 1, DepartmentId = departmentId, PassportId = 2 }
        };
        var passport = new Passport { Id = 2, Type = "Passport", Number = "1234567890" };
        var department = new Department { Id = departmentId, CompanyId = 1, Name = "Test Department", Phone = "+1234567890" };
        _employeeRepositoryMock.Setup(repo => repo.GetEmployeesByDepartmentIdAsync(departmentId, default)).ReturnsAsync(employees);
        _passportRepositoryMock.Setup(repo => repo.GetPassportAsync(It.IsAny<int>(), default)).ReturnsAsync(passport);
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(It.IsAny<int>(), default)).ReturnsAsync(department);

        // Act
        var result = await _employeeService.GetEmployeesByDepartmentIdAsync(departmentId);

        // Assert
        Assert.Single(result);
        Assert.Equal(employees[0].Name, result[0].Name);
        Assert.Equal(passport.Type, result[0].Passport.Type);
        Assert.Equal(department.Name, result[0].Department.Name);
        _employeeRepositoryMock.Verify(repo => repo.GetEmployeesByDepartmentIdAsync(departmentId, default), Times.Once);
        _passportRepositoryMock.Verify(repo => repo.GetPassportAsync(It.IsAny<int>(), default), Times.Once);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(It.IsAny<int>(), default), Times.Once);
    }
}