using EmployeeBenefits.Type;
using EmployeeBenefits.Type.Rule;

namespace EmployeeBenefits.Impl
{
    public class DisountCalculator : IRuleEvaluator
    {
        private readonly IRulesService _rulesService;

        public DisountCalculator(IRulesService rulesService)
        {
            _rulesService = rulesService;
        }

        public decimal GetTotalDiscount(Employee employee)
        {
            decimal totalDiscount = 0;
            var rules = _rulesService.GetAllRules();

            foreach (var rule in rules)
            {
                totalDiscount += rule.GetDiscount(employee);
            }

            return totalDiscount;
        }
    }
}