using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EmployeeBenefits.Impl;
using EmployeeBenefits.Impl.Rule;
using EmployeeBenefits.Type;
using EmployeeBenefits.Type.Data;
using EmployeeBenefits.Type.Rule;
using Moq;
using Xunit;

namespace BenefitCostCalculator.Test
{
    public class EmployeeServiceTest
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeServiceTest()
        {
            _employeeRepository = new MemoryRepository<Employee>();
        }

        [Fact]
        public void Should_get_instance()
        {
            IRepository<Employee> employeeRepository = new MemoryRepository<Employee>();
            IEmployeeService service = new EmployeeService(employeeRepository);
            Assert.True(service != null);
        }

        [Fact]
        public void Should_get_an_employee_by_id()
        {
            IEmployeeService service = new EmployeeService(_employeeRepository);
            int employeeId = 1;

            var employee = service.Get(employeeId);

            Assert.NotNull(employee);
        }
    }

    public interface IEmployeeService
    {
        Employee Get(int employeeId);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee Get(int employeeId)
        {
            return _employeeRepository.Get(employeeId);
        }
    }
}