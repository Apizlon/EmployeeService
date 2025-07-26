namespace EmployeeService.Domain.Exceptions;

/// <inheritdoc/>
public class CompanyNotFoundException : NotFoundException
{
    /// <inheritdoc/>
    public CompanyNotFoundException(int id) : base($"Company with id {id} not found.")
    {
        
    }
}