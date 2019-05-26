using System.Collections.Generic;

namespace EmployeeBenefits.Type
{
    public interface IEmployeeService
    {
        Employee Get(string employeeId);
        IEnumerable<Employee> All();
    }
}