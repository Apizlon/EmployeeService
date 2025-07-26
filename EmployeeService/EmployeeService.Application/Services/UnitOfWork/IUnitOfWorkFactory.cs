namespace EmployeeService.Application.Services.UnitOfWork;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create(OnDispose onDispose = OnDispose.Rollback);
}