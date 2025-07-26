using EmployeeService.Application.Services.UnitOfWork;

namespace EmployeeService.Application.Services;

/// <inheritdoc/>
public class DepartmentService : IDepartmentService
{
    /// <summary><see cref="IUnitOfWorkFactory"/>.</summary>
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="unitOfWorkFactory"><see cref="IUnitOfWorkFactory"/>.</param>
    public DepartmentService(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
    }
}