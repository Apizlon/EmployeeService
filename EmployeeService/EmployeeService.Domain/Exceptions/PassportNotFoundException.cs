namespace EmployeeService.Domain.Exceptions;

/// <inheritdoc/>
public class PassportNotFoundException : NotFoundException
{
    /// <inheritdoc/>
    public PassportNotFoundException(int id) : base($"Passport with id {id} not found.")
    {
        
    }
}