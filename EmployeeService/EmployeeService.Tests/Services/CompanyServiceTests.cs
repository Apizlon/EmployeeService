using EmployeeService.Application.Contracts.Company;
using EmployeeService.Application.Services;
using EmployeeService.DataAccess.Repositories;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Exceptions;
using Moq;

namespace EmployeeService.Tests.Services;

public class CompanyServiceTests
{
    private readonly Mock<ICompanyRepository> _companyRepositoryMock;
    private readonly ICompanyService _companyService;

    public CompanyServiceTests()
    {
        _companyRepositoryMock = new Mock<ICompanyRepository>();
        _companyService = new CompanyService(_companyRepositoryMock.Object);
    }

    [Fact]
    public async Task AddCompanyAsync_WithValidRequest_ShouldReturnId()
    {
        // Arrange
        int expectedId = 1;
        var companyRequest = new CompanyRequest { Name = "Test Company" };
        _companyRepositoryMock.Setup(repo => repo.AddCompanyAsync(It.IsAny<Company>())).ReturnsAsync(expectedId);

        // Act
        var result = await _companyService.AddCompanyAsync(companyRequest);

        // Assert
        Assert.Equal(expectedId, result);
        _companyRepositoryMock.Verify(repo => repo.AddCompanyAsync(It.IsAny<Company>()), Times.Once);
    }

    [Fact]
    public async Task AddCompanyAsync_WithEmptyName_ShouldThrowBadRequestException()
    {
        // Arrange
        var companyRequest = new CompanyRequest { Name = "" };

        // Act
        var func = async () => await _companyService.AddCompanyAsync(companyRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
    }

    [Fact]
    public async Task AddCompanyAsync_WithWhitespaceName_ShouldThrowBadRequestException()
    {
        // Arrange
        var companyRequest = new CompanyRequest { Name = "   " };

        // Act
        var func = async () => await _companyService.AddCompanyAsync(companyRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
    }

    [Fact]
    public async Task AddCompanyAsync_WithNameExceeding255Characters_ShouldThrowBadRequestException()
    {
        // Arrange
        var companyRequest = new CompanyRequest { Name = new string('A', 256) };

        // Act
        var func = async () => await _companyService.AddCompanyAsync(companyRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
    }

    [Fact]
    public async Task GetCompanyAsync_WithValidId_ShouldReturnCompanyResponse()
    {
        // Arrange
        int id = 1;
        var company = new Company { Id = id, Name = "Test Company" };
        _companyRepositoryMock.Setup(repo => repo.GetCompanyAsync(id, default)).ReturnsAsync(company);

        // Act
        var result = await _companyService.GetCompanyAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(company.Id, result.Id);
        Assert.Equal(company.Name, result.Name);
        _companyRepositoryMock.Verify(repo => repo.GetCompanyAsync(id, default), Times.Once);
    }

    [Fact]
    public async Task GetCompanyAsync_WithInvalidId_ShouldThrowCompanyNotFoundException()
    {
        // Arrange
        int id = 1;
        _companyRepositoryMock.Setup(repo => repo.GetCompanyAsync(id, default)).ReturnsAsync((Company?)null);

        // Act
        var func = async () => await _companyService.GetCompanyAsync(id);

        // Assert
        await Assert.ThrowsAsync<CompanyNotFoundException>(func);
    }

    [Fact]
    public async Task DeleteCompanyAsync_WithValidId_ShouldSucceed()
    {
        // Arrange
        int id = 1;
        _companyRepositoryMock.Setup(repo => repo.CompanyExistsAsync(id, CancellationToken.None)).ReturnsAsync(true);

        // Act
        await _companyService.DeleteCompanyAsync(id);

        // Assert
        _companyRepositoryMock.Verify(repo => repo.CompanyExistsAsync(id, CancellationToken.None), Times.Once);
        _companyRepositoryMock.Verify(repo => repo.DeleteCompanyAsync(id), Times.Once);
    }

    [Fact]
    public async Task DeleteCompanyAsync_WithInvalidId_ShouldThrowCompanyNotFoundException()
    {
        // Arrange
        int id = 1;
        _companyRepositoryMock.Setup(repo => repo.CompanyExistsAsync(id, CancellationToken.None)).ReturnsAsync(false);

        // Act
        var func = async () => await _companyService.DeleteCompanyAsync(id);

        // Assert
        await Assert.ThrowsAsync<CompanyNotFoundException>(func);
    }

    [Fact]
    public async Task UpdateCompanyAsync_WithValidRequest_ShouldSucceed()
    {
        // Arrange
        int id = 1;
        var companyRequest = new CompanyRequest { Name = "Updated Company" };
        _companyRepositoryMock.Setup(repo => repo.CompanyExistsAsync(id, CancellationToken.None)).ReturnsAsync(true);

        // Act
        await _companyService.UpdateCompanyAsync(id, companyRequest);

        // Assert
        _companyRepositoryMock.Verify(repo => repo.CompanyExistsAsync(id, CancellationToken.None), Times.Once);
        _companyRepositoryMock.Verify(repo => repo.UpdateCompanyAsync(It.IsAny<Company>()), Times.Once);
    }

    [Fact]
    public async Task UpdateCompanyAsync_WithInvalidId_ShouldThrowCompanyNotFoundException()
    {
        // Arrange
        int id = 1;
        var companyRequest = new CompanyRequest { Name = "Updated Company" };
        _companyRepositoryMock.Setup(repo => repo.CompanyExistsAsync(id, CancellationToken.None)).ReturnsAsync(false);

        // Act
        var func = async () => await _companyService.UpdateCompanyAsync(id, companyRequest);

        // Assert
        await Assert.ThrowsAsync<CompanyNotFoundException>(func);
    }

    [Fact]
    public async Task UpdateCompanyAsync_WithEmptyName_ShouldThrowBadRequestException()
    {
        // Arrange
        int id = 1;
        var companyRequest = new CompanyRequest { Name = "" };
        _companyRepositoryMock.Setup(repo => repo.CompanyExistsAsync(id, CancellationToken.None)).ReturnsAsync(true);

        // Act
        var func = async () => await _companyService.UpdateCompanyAsync(id, companyRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
    }

    [Fact]
    public async Task UpdateCompanyAsync_WithWhitespaceName_ShouldThrowBadRequestException()
    {
        // Arrange
        int id = 1;
        var companyRequest = new CompanyRequest { Name = "   " };
        _companyRepositoryMock.Setup(repo => repo.CompanyExistsAsync(id, CancellationToken.None)).ReturnsAsync(true);

        // Act
        var func = async () => await _companyService.UpdateCompanyAsync(id, companyRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
    }

    [Fact]
    public async Task UpdateCompanyAsync_WithNameExceeding255Characters_ShouldThrowBadRequestException()
    {
        // Arrange
        int id = 1;
        var companyRequest = new CompanyRequest { Name = new string('A', 256) };
        _companyRepositoryMock.Setup(repo => repo.CompanyExistsAsync(id, CancellationToken.None)).ReturnsAsync(true);

        // Act
        var func = async () => await _companyService.UpdateCompanyAsync(id, companyRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
    }
}