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
    public class CostCalculatorTest
    {
        private List<Employee> _employees;
        private IEnumerable<IDiscountRule> _discountRules;

        public CostCalculatorTest()
        {
            _employees = SetupEmployees();
            _discountRules = SetupDiscountRules();
        }

        private IEnumerable<IDiscountRule> SetupDiscountRules()
        {
            return new List<IDiscountRule>()
            {
                new NameStartingWithTheLetterA(1000, 500, (decimal) .1)
            };
        }

        private List<Employee> SetupEmployees()
        {
            return new List<Employee>()
            {
                new Employee()
                {
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
                    Name = "Eric Evans",
                    Salary = 52000,
                    Dependents = new List<string>
                    {
                        "Lisa Evans"
                    }
                },
                new Employee()
                {
                    Name = "Alan Turing",
                    Salary = 52000
                }
            };
        }

        [Fact]
        public void TotalDiscount_should_be_100_for_employee_name_starting_with_the_letter_A_with_no_dependents()
        {
            var employee = _employees[3];
            var mockRuleRepository = new Mock<IRuleRepository>();
            mockRuleRepository.Setup(x => x.GetAll()).Returns(_discountRules);
            IRulesService rulesService = new RulesService(mockRuleRepository.Object);
            IRuleEvaluator service = new DisountCalculator(rulesService);

            decimal totalDiscount = service.GetTotalDiscount(employee);

            Assert.True(totalDiscount == 100);
        }

        [Fact]
        public void TotalDiscount_should_be_0_for_employee_name_not_starting_with_the_letter_A()
        {
            var employee = _employees[2];
            var mockRuleRepository = new Mock<IRuleRepository>();
            mockRuleRepository.Setup(x => x.GetAll()).Returns(_discountRules);
            IRulesService rulesService = new RulesService(mockRuleRepository.Object);
            IRuleEvaluator service = new DisountCalculator(rulesService);

            decimal totalDiscount = service.GetTotalDiscount(employee);

            Assert.True(totalDiscount == 0);
        }

        [Fact]
        public void TotalDiscount_should_be_50_for_1_employee_dependent_with_name_starting_with_the_letter_A()
        {
            var employee = _employees[0];
            var mockRuleRepository = new Mock<IRuleRepository>();
            mockRuleRepository.Setup(x => x.GetAll()).Returns(_discountRules);
            IRulesService rulesService = new RulesService(mockRuleRepository.Object);
            IRuleEvaluator service = new DisountCalculator(rulesService);

            decimal totalDiscount = service.GetTotalDiscount(employee);

            Assert.True(totalDiscount == 50);
        }
        
        [Fact]
        public void Calculate_employee_benefit_cost_per_paycheck()
        {
            var employee = _employees[0];
            int numberOfAnnualPaychecks = 26;
            var paycheckValue = employee.Salary / numberOfAnnualPaychecks; // $2,000

            decimal baseAnnualCostPerEmployee = 1000;
            decimal annualCostPerDependent = 500;
            decimal totalAnnualCost = 0;

            //calculate totalAnnalCost
            totalAnnualCost = baseAnnualCostPerEmployee + (employee.Dependents.Count * annualCostPerDependent);

            // 10% discount rule
            if (employee.Name.ToLower().StartsWith("a"))
            {
                var discountAmount = baseAnnualCostPerEmployee * ((decimal).1);
                totalAnnualCost -= discountAmount;
            }

            var numberOfDependentsEligibleForDiscount = employee
                .Dependents
                .Count(name => name.ToLower().StartsWith("a"));

            totalAnnualCost -= (annualCostPerDependent * (decimal).1) * numberOfDependentsEligibleForDiscount;

            var costPerPaycheck = totalAnnualCost / 26;

            Assert.True(totalAnnualCost == 1950);
            Assert.True(costPerPaycheck == 75);
        }
    }
}
