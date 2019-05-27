using System;
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
        private List<QuoteParameter> _quoteParameters;
        private IEnumerable<IDiscountRule> _discountRules;
        private Func<string, IDiscountRule[]> ruleAccessor;

        public CostCalculatorTest()
        {
            _employees = SetupEmployees();
            _quoteParameters = SetupQuoteParams();
            _discountRules = SetupDiscountRules();

            ruleAccessor = key =>
            {
                switch (key)
                {
                    case "NameStartingWithTheLetterA":
                        return new[] {new NameStartingWithTheLetterA(1000, 500, (decimal) .1)};
                    case "All":
                        return new[] { new NameStartingWithTheLetterA(1000, 500, (decimal).1) };
                    default:
                        throw new KeyNotFoundException(); // or maybe return null, up to you
                }
            };
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

        private List<QuoteParameter> SetupQuoteParams()
        {
            return new List<QuoteParameter>()
            {
                new QuoteParameter()
                {
                    EmployeeName = "Robert Martin",
                    Dependents = new string[]
                    {
                        "Ava Martin",
                        "Eloise Martin"
                    }
                    
                },
                new QuoteParameter()
                {
                    EmployeeName = "Martin Fowler",
                    Dependents = new string[]
                    {
                        "Martha Fowler",
                        "Ben Fowler",
                        "Alex Fowler",
                    }
                },
                new QuoteParameter()
                {
                    EmployeeName = "Eric Evans",
                    Dependents = new string[]
                    {
                        "Lisa Evans"
                    }
                },
                new QuoteParameter()
                {
                    EmployeeName = "Alan Turing"
                }
            };
        }

        [Fact]
        public void TotalDiscount_should_be_100_for_employee_name_starting_with_the_letter_A_with_no_dependents()
        {
            var employee = _employees[3];
            var quoteParam = _quoteParameters[3];
            var mockRuleRepository = new Mock<IRuleRepository>();
            mockRuleRepository.Setup(x => x.GetAll()).Returns(_discountRules);
            IRulesService rulesService = new RulesService(mockRuleRepository.Object);
            IRuleEvaluator service = new DiscountCalculator(ruleAccessor);

            var quote = service.GetQuote(employee, quoteParam.Dependents);

            Assert.True(quote.TotalDiscounts == 100);
        }

        [Fact]
        public void TotalDiscount_should_be_0_for_employee_name_not_starting_with_the_letter_A()
        {
            var employee = _employees[2];
            var quoteParam = _quoteParameters[2];
            var mockRuleRepository = new Mock<IRuleRepository>();
            mockRuleRepository.Setup(x => x.GetAll()).Returns(_discountRules);
            IRulesService rulesService = new RulesService(mockRuleRepository.Object);
            IRuleEvaluator service = new DiscountCalculator(ruleAccessor);

            var quote = service.GetQuote(employee, quoteParam.Dependents);

            Assert.True(quote.TotalDiscounts == 0);
        }

        [Fact]
        public void TotalDiscount_should_be_50_for_1_employee_dependent_with_name_starting_with_the_letter_A()
        {
            var employee = _employees[0];
            var quoteParam = _quoteParameters[0];
            var mockRuleRepository = new Mock<IRuleRepository>();
            mockRuleRepository.Setup(x => x.GetAll()).Returns(_discountRules);
            IRuleEvaluator service = new DiscountCalculator(ruleAccessor);

            var quote = service.GetQuote(employee, quoteParam.Dependents);

            Assert.True(quote.TotalDiscounts == 50);
        }
        
        /* Work out the math before implemation */
        [Fact]
        public void Calculate_employee_benefit_cost_per_paycheck()
        {
            var employee = _employees[0];
            var quoteParameter = _quoteParameters[0];
            int numberOfAnnualPaychecks = 26;
            var paycheckValue = employee.Salary / numberOfAnnualPaychecks; // $2,000

            decimal baseAnnualCostPerEmployee = 1000;
            decimal annualCostPerDependent = 500;
            decimal totalAnnualCost = 0;

            //calculate totalAnnalCost
            totalAnnualCost = baseAnnualCostPerEmployee + (quoteParameter.Dependents.Length * annualCostPerDependent);

            // 10% discount rule
            if (quoteParameter.EmployeeName.ToLower().StartsWith("a"))
            {
                var discountAmount = baseAnnualCostPerEmployee * ((decimal).1);
                totalAnnualCost -= discountAmount;
            }

            var numberOfDependentsEligibleForDiscount = quoteParameter
                .Dependents
                .Count(name => name.ToLower().StartsWith("a"));

            totalAnnualCost -= (annualCostPerDependent * (decimal).1) * numberOfDependentsEligibleForDiscount;

            var costPerPaycheck = totalAnnualCost / 26;

            Assert.True(totalAnnualCost == 1950);
            Assert.True(costPerPaycheck == 75);
        }
    }
}
