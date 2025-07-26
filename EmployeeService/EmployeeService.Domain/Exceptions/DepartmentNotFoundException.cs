namespace EmployeeService.Domain.Exceptions;

/// <inheritdoc/>
public class DepartmentNotFoundException : NotFoundException
{
    /// <inheritdoc/>
    public DepartmentNotFoundException(int id) : base($"Department with id {id} not found.")
    {
        
    }
}