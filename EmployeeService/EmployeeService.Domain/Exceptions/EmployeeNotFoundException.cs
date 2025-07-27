namespace EmployeeService.Domain.Exceptions;

/// <inheritdoc/>
public class EmployeeNotFoundException : NotFoundException
{
    /// <inheritdoc/>
    public EmployeeNotFoundException(int id) : base($"Employee with id {id} not found.")
    {
        
    }
}