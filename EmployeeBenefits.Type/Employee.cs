using System.Collections.Generic;

namespace EmployeeBenefits.Type
{
    public class Employee
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public List<string> Dependents { get; set; }
    }
}