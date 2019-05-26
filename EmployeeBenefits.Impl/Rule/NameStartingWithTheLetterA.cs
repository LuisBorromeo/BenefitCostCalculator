using System.Linq;
using EmployeeBenefits.Type;
using EmployeeBenefits.Type.Rule;

namespace EmployeeBenefits.Impl.Rule
{
    public class NameStartingWithTheLetterA : IDiscountRule
    {
        private readonly decimal _baseAnnualCostPerEmployee;
        private readonly decimal _annualCostPerDependent;
        private readonly decimal _discountPercent;

        public NameStartingWithTheLetterA(decimal baseAnnualCostPerEmployee, decimal annualCostPerDependent, decimal discountPercent)
        {
            _baseAnnualCostPerEmployee = baseAnnualCostPerEmployee;
            _annualCostPerDependent = annualCostPerDependent;
            _discountPercent = discountPercent;
        }

        public decimal GetDiscount(Employee employee)
        {
            decimal totalDiscount = 0;

            // 10% discount rule
            if (employee.Name.Trim().ToLower().StartsWith("a"))
            {
                totalDiscount += _baseAnnualCostPerEmployee * _discountPercent;
            }

            if (employee.Dependents != null)
            {
                var numberOfDependentsEligibleForDiscount = employee?.Dependents
                    .Count(name => name.Trim().ToLower().StartsWith("a"));

                totalDiscount += (_annualCostPerDependent * _discountPercent) * (decimal) numberOfDependentsEligibleForDiscount;
            }

            return totalDiscount;
        }
    }
}