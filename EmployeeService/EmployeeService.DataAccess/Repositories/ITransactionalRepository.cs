using System.Data;

namespace EmployeeService.DataAccess.Repositories;

public interface ITransactionalRepository
{
    void SetTransaction(IDbTransaction transaction);
}