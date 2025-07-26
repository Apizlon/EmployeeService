namespace EmployeeService.Application.Services.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
    T GetRepository<T>() where T : class;
} 