using SS23Tiers.Api.Models;

namespace SS23Tiers.Api.Service;

public interface IEmployeeService
{
	Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(Guid Id);
    Task<bool> Save(Employee employee);
    Task<bool> Update(Employee employee,Guid Id);
    Task<bool> Delete(Guid Id);
}

