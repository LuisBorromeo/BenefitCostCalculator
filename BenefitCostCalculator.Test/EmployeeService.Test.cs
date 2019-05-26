using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeBenefits.Impl.Encoding;
using EmployeeBenefits.Type;
using EmployeeBenefits.Type.Data;
using Xunit;

namespace BenefitCostCalculator.Test
{
    public class EmployeeServiceTest
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly List<Employee> _allEmployees;

        public EmployeeServiceTest()
        {
            _employeeRepository = new MemoryRepository<Employee>();
            _allEmployees = SetupEmployees();
            _allEmployees.ForEach(empl => _employeeRepository.Save(empl.Id, empl));
        }

        private List<Employee> SetupEmployees()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    Id = Guid.NewGuid().ToShortString(),
                    Name = "Robert Martin",
                    Salary = 52000,
                    Dependents = new List<string>
                    {
                        "Ava Martin",
                        "Eloise Martin"
                    }

                },
                new Employee()
                {
                    Id = Guid.NewGuid().ToShortString(),
                    Name = "Martin Fowler",
                    Salary = 52000,
                    Dependents = new List<string>
                    {
                        "Martha Fowler",
                        "Ben Fowler",
                        "Alex Fowler",
                    }
                },
                new Employee()
                {
                    Id = Guid.NewGuid().ToShortString(),
                    Name = "Eric Evans",
                    Salary = 52000,
                    Dependents = new List<string>
                    {
                        "Lisa Evans"
                    }
                },
                new Employee()
                {
                    Id = Guid.NewGuid().ToShortString(),
                    Name = "Alan Turing",
                    Salary = 52000
                }
            };
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
            string employeeId = _employeeRepository.First().Id;

            var employee = service.Get(employeeId);

            Assert.NotNull(employee);
        }

        [Fact]
        public void Should_return_null_user_can_not_be_found()
        {
            IEmployeeService service = new EmployeeService(_employeeRepository);

            var employee = service.Get("unkown");

            Assert.Null(employee);
        }

        [Fact]
        public void Should_get_all_employee()
        {
            IEmployeeService service = new EmployeeService(_employeeRepository);

            var employees = service.All();

            Assert.True(employees.Any());
            Assert.True(employees.Count() == _allEmployees.Count());
        }
    }
}