using System;
using Microsoft.EntityFrameworkCore;
using SS23Tiers.Api.Data;
using SS23Tiers.Api.Models;

namespace SS23Tiers.Api.Service
{
	public class EmployeeService:IEmployeeService,IDisposable
	{
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
		{
            _context = context;
		}

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                //Find record
                var emp = await _context.Employee.FindAsync(Id);
                if (emp is null) return false;
                _context.Employee.Remove(emp);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<Employee> GetEmployee(Guid Id)
            => await _context.Employee.FindAsync(Id);

        public async Task<List<Employee>> GetEmployees()
            => await _context.Employee.Include("Position").Include("Department").ToListAsync();

        public async Task<bool> Save(Employee employee)
        {
            try
            {
                employee.EmployeeId = Guid.NewGuid();
                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Employee employee, Guid Id)
        {
            try
            {
                //Find record
                var emp = await _context.Employee.FindAsync(Id);
                if (emp is null) return false;
                _context.Employee.Attach(emp);
                emp.Address = employee.Address;
                emp.DateOfBirth = employee.DateOfBirth;
                emp.DepartmentId = employee.DepartmentId;
                emp.EmployeeName = employee.EmployeeName;
                emp.PhoneNumber = employee.PhoneNumber;
                emp.PlaceOfBirth = employee.PlaceOfBirth;
                emp.PositionId = employee.PositionId;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

