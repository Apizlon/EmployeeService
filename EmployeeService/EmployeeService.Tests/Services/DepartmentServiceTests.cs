using EmployeeService.Application.Contracts.Department;
using EmployeeService.Application.Interfaces.Repositories;
using EmployeeService.Application.Interfaces.Services;
using EmployeeService.Application.Services;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Exceptions;
using Moq;

namespace EmployeeService.Tests.Services;

public class DepartmentServiceTests
{
    private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
    private readonly Mock<ICompanyRepository> _companyRepositoryMock;
    private readonly IDepartmentService _departmentService;

    public DepartmentServiceTests()
    {
        _departmentRepositoryMock = new Mock<IDepartmentRepository>();
        _companyRepositoryMock = new Mock<ICompanyRepository>();
        _departmentService = new DepartmentService(_departmentRepositoryMock.Object, _companyRepositoryMock.Object);
    }

    [Fact]
    public async Task AddDepartmentAsync_WithValidRequest_ShouldReturnId()
    {
        // Arrange
        int expectedId = 1;
        var departmentRequest = new AddDepartmentRequest
            { CompanyId = 1, Name = "Test Department", Phone = "+1234567890" };
        _companyRepositoryMock
            .Setup(repo => repo.CompanyExistsAsync(departmentRequest.CompanyId, CancellationToken.None))
            .ReturnsAsync(true);
        _departmentRepositoryMock.Setup(repo => repo.AddDepartmentAsync(It.IsAny<Department>()))
            .ReturnsAsync(expectedId);

        // Act
        var result = await _departmentService.AddDepartmentAsync(departmentRequest);

        // Assert
        Assert.Equal(expectedId, result);
        _companyRepositoryMock.Verify(
            repo => repo.CompanyExistsAsync(departmentRequest.CompanyId, CancellationToken.None), Times.Once);
        _departmentRepositoryMock.Verify(repo => repo.AddDepartmentAsync(It.IsAny<Department>()), Times.Once);
    }

    [Fact]
    public async Task AddDepartmentAsync_WithInvalidCompanyId_ShouldThrowCompanyNotFoundException()
    {
        // Arrange
        var departmentRequest = new AddDepartmentRequest
            { CompanyId = 1, Name = "Test Department", Phone = "+1234567890" };
        _companyRepositoryMock
            .Setup(repo => repo.CompanyExistsAsync(departmentRequest.CompanyId, CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var func = async () => await _departmentService.AddDepartmentAsync(departmentRequest);

        // Assert
        await Assert.ThrowsAsync<CompanyNotFoundException>(func);
        _companyRepositoryMock.Verify(
            repo => repo.CompanyExistsAsync(departmentRequest.CompanyId, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddDepartmentAsync_WithEmptyName_ShouldThrowBadRequestException()
    {
        // Arrange
        var departmentRequest = new AddDepartmentRequest { CompanyId = 1, Name = "", Phone = "+1234567890" };
        _companyRepositoryMock
            .Setup(repo => repo.CompanyExistsAsync(departmentRequest.CompanyId, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var func = async () => await _departmentService.AddDepartmentAsync(departmentRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _companyRepositoryMock.Verify(
            repo => repo.CompanyExistsAsync(departmentRequest.CompanyId, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddDepartmentAsync_WithNameExceeding255Characters_ShouldThrowBadRequestException()
    {
        // Arrange
        var departmentRequest = new AddDepartmentRequest
            { CompanyId = 1, Name = new string('A', 256), Phone = "+1234567890" };
        _companyRepositoryMock
            .Setup(repo => repo.CompanyExistsAsync(departmentRequest.CompanyId, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var func = async () => await _departmentService.AddDepartmentAsync(departmentRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _companyRepositoryMock.Verify(
            repo => repo.CompanyExistsAsync(departmentRequest.CompanyId, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddDepartmentAsync_WithInvalidPhone_ShouldThrowBadRequestException()
    {
        // Arrange
        var departmentRequest = new AddDepartmentRequest
            { CompanyId = 1, Name = "Test Department", Phone = "invalid-phone" };
        _companyRepositoryMock
            .Setup(repo => repo.CompanyExistsAsync(departmentRequest.CompanyId, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var func = async () => await _departmentService.AddDepartmentAsync(departmentRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _companyRepositoryMock.Verify(
            repo => repo.CompanyExistsAsync(departmentRequest.CompanyId, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddDepartmentAsync_WithPhoneExceeding50Characters_ShouldThrowBadRequestException()
    {
        // Arrange
        var departmentRequest = new AddDepartmentRequest
            { CompanyId = 1, Name = "Test Department", Phone = "+1234567890" + new string('1', 41) };
        _companyRepositoryMock
            .Setup(repo => repo.CompanyExistsAsync(departmentRequest.CompanyId, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var func = async () => await _departmentService.AddDepartmentAsync(departmentRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _companyRepositoryMock.Verify(
            repo => repo.CompanyExistsAsync(departmentRequest.CompanyId, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetDepartmentAsync_WithValidId_ShouldReturnDepartmentResponse()
    {
        // Arrange
        int id = 1;
        var department = new Department { Id = id, CompanyId = 1, Name = "Test Department", Phone = "+1234567890" };
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(id, default)).ReturnsAsync(department);

        // Act
        var result = await _departmentService.GetDepartmentAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(department.Name, result.Name);
        Assert.Equal(department.Phone, result.Phone);
        _departmentRepositoryMock.Verify(repo => repo.GetDepartmentAsync(id, default), Times.Once);
    }

    [Fact]
    public async Task GetDepartmentAsync_WithInvalidId_ShouldThrowDepartmentNotFoundException()
    {
        // Arrange
        int id = 1;
        _departmentRepositoryMock.Setup(repo => repo.GetDepartmentAsync(id, default)).ReturnsAsync((Department?)null);

        // Act
        var func = async () => await _departmentService.GetDepartmentAsync(id);

        // Assert
        await Assert.ThrowsAsync<DepartmentNotFoundException>(func);
    }

    [Fact]
    public async Task DeleteDepartmentAsync_WithValidId_ShouldSucceed()
    {
        // Arrange
        int id = 1;
        _departmentRepositoryMock.Setup(repo => repo.DepartmentExistsAsync(id, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        await _departmentService.DeleteDepartmentAsync(id);

        // Assert
        _departmentRepositoryMock.Verify(repo => repo.DepartmentExistsAsync(id, CancellationToken.None), Times.Once);
        _departmentRepositoryMock.Verify(repo => repo.DeleteDepartmentAsync(id), Times.Once);
    }

    [Fact]
    public async Task DeleteDepartmentAsync_WithInvalidId_ShouldThrowDepartmentNotFoundException()
    {
        // Arrange
        int id = 1;
        _departmentRepositoryMock.Setup(repo => repo.DepartmentExistsAsync(id, CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var func = async () => await _departmentService.DeleteDepartmentAsync(id);

        // Assert
        await Assert.ThrowsAsync<DepartmentNotFoundException>(func);
    }

    [Fact]
    public async Task UpdateDepartmentAsync_WithValidRequest_ShouldSucceed()
    {
        // Arrange
        int id = 1;
        var departmentRequest = new UpdateDepartmentRequest
            { CompanyId = 1, Name = "Updated Department", Phone = "+1234567890" };
        _departmentRepositoryMock.Setup(repo => repo.DepartmentExistsAsync(id, CancellationToken.None))
            .ReturnsAsync(true);
        _companyRepositoryMock
            .Setup(repo => repo.CompanyExistsAsync(departmentRequest.CompanyId!.Value, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        await _departmentService.UpdateDepartmentAsync(id, departmentRequest);

        // Assert
        _departmentRepositoryMock.Verify(repo => repo.DepartmentExistsAsync(id, CancellationToken.None), Times.Once);
        _companyRepositoryMock.Verify(
            repo => repo.CompanyExistsAsync(departmentRequest.CompanyId!.Value, CancellationToken.None), Times.Once);
        _departmentRepositoryMock.Verify(repo => repo.UpdateDepartmentAsync(It.IsAny<Department>()), Times.Once);
    }

    [Fact]
    public async Task UpdateDepartmentAsync_WithInvalidDepartmentId_ShouldThrowDepartmentNotFoundException()
    {
        // Arrange
        int id = 1;
        var departmentRequest = new UpdateDepartmentRequest
            { CompanyId = 1, Name = "Updated Department", Phone = "+1234567890" };
        _departmentRepositoryMock.Setup(repo => repo.DepartmentExistsAsync(id, CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var func = async () => await _departmentService.UpdateDepartmentAsync(id, departmentRequest);

        // Assert
        await Assert.ThrowsAsync<DepartmentNotFoundException>(func);
    }

    [Fact]
    public async Task UpdateDepartmentAsync_WithInvalidCompanyId_ShouldThrowCompanyNotFoundException()
    {
        // Arrange
        int id = 1;
        var departmentRequest = new UpdateDepartmentRequest
            { CompanyId = 1, Name = "Updated Department", Phone = "+1234567890" };
        _departmentRepositoryMock.Setup(repo => repo.DepartmentExistsAsync(id, CancellationToken.None))
            .ReturnsAsync(true);
        _companyRepositoryMock
            .Setup(repo => repo.CompanyExistsAsync(departmentRequest.CompanyId!.Value, CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var func = async () => await _departmentService.UpdateDepartmentAsync(id, departmentRequest);

        // Assert
        await Assert.ThrowsAsync<CompanyNotFoundException>(func);
    }

    [Fact]
    public async Task UpdateDepartmentAsync_WithEmptyName_ShouldThrowBadRequestException()
    {
        // Arrange
        int id = 1;
        var departmentRequest = new UpdateDepartmentRequest { CompanyId = 1, Name = "", Phone = "+1234567890" };
        _departmentRepositoryMock.Setup(repo => repo.DepartmentExistsAsync(id, CancellationToken.None))
            .ReturnsAsync(true);
        _companyRepositoryMock
            .Setup(repo => repo.CompanyExistsAsync(departmentRequest.CompanyId!.Value, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var func = async () => await _departmentService.UpdateDepartmentAsync(id, departmentRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _companyRepositoryMock.Verify(
            repo => repo.CompanyExistsAsync(departmentRequest.CompanyId!.Value, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task UpdateDepartmentAsync_WithNameExceeding255Characters_ShouldThrowBadRequestException()
    {
        // Arrange
        int id = 1;
        var departmentRequest = new UpdateDepartmentRequest
            { CompanyId = 1, Name = new string('A', 256), Phone = "+1234567890" };
        _departmentRepositoryMock.Setup(repo => repo.DepartmentExistsAsync(id, CancellationToken.None))
            .ReturnsAsync(true);
        _companyRepositoryMock
            .Setup(repo => repo.CompanyExistsAsync(departmentRequest.CompanyId!.Value, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var func = async () => await _departmentService.UpdateDepartmentAsync(id, departmentRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _companyRepositoryMock.Verify(
            repo => repo.CompanyExistsAsync(departmentRequest.CompanyId!.Value, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task UpdateDepartmentAsync_WithInvalidPhone_ShouldThrowBadRequestException()
    {
        // Arrange
        int id = 1;
        var departmentRequest = new UpdateDepartmentRequest
            { CompanyId = 1, Name = "Updated Department", Phone = "invalid-phone" };
        _departmentRepositoryMock.Setup(repo => repo.DepartmentExistsAsync(id, CancellationToken.None))
            .ReturnsAsync(true);
        _companyRepositoryMock
            .Setup(repo => repo.CompanyExistsAsync(departmentRequest.CompanyId!.Value, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var func = async () => await _departmentService.UpdateDepartmentAsync(id, departmentRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _companyRepositoryMock.Verify(
            repo => repo.CompanyExistsAsync(departmentRequest.CompanyId!.Value, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task UpdateDepartmentAsync_WithPhoneExceeding50Characters_ShouldThrowBadRequestException()
    {
        // Arrange
        int id = 1;
        var departmentRequest = new UpdateDepartmentRequest
            { CompanyId = 1, Name = "Updated Department", Phone = "+1234567890" + new string('1', 41) };
        _departmentRepositoryMock.Setup(repo => repo.DepartmentExistsAsync(id, CancellationToken.None))
            .ReturnsAsync(true);
        _companyRepositoryMock
            .Setup(repo => repo.CompanyExistsAsync(departmentRequest.CompanyId!.Value, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var func = async () => await _departmentService.UpdateDepartmentAsync(id, departmentRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _companyRepositoryMock.Verify(
            repo => repo.CompanyExistsAsync(departmentRequest.CompanyId!.Value, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task UpdateDepartmentAsync_WithNullFields_ShouldSucceed()
    {
        // Arrange
        int id = 1;
        var departmentRequest = new UpdateDepartmentRequest { CompanyId = null, Name = null, Phone = null };
        _departmentRepositoryMock.Setup(repo => repo.DepartmentExistsAsync(id, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        await _departmentService.UpdateDepartmentAsync(id, departmentRequest);

        // Assert
        _departmentRepositoryMock.Verify(repo => repo.DepartmentExistsAsync(id, CancellationToken.None), Times.Once);
        _departmentRepositoryMock.Verify(repo => repo.UpdateDepartmentAsync(It.IsAny<Department>()), Times.Once);
        _companyRepositoryMock.Verify(repo => repo.CompanyExistsAsync(It.IsAny<int>(), CancellationToken.None),
            Times.Never);
    }
}