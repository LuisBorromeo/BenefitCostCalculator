using System.Collections.Generic;
using System.Linq;
using EmployeeBenefits.Type;
using EmployeeBenefits.Type.Data;

namespace BenefitCostCalculator.Test
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee Get(string employeeId)
        {
            return _employeeRepository.Get(employeeId);
        }

        public IEnumerable<Employee> All()
        {
            return _employeeRepository.ToList();
        }
    }
}