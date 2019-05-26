using System.Collections.Generic;
using EmployeeBenefits.Type.Data;
using EmployeeBenefits.Type.Rule;

namespace BenefitCostCalculator.Test
{
    public class RulesService : IRulesService
    {
        private readonly IRuleRepository _ruleRepository;

        public RulesService(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }

        public IEnumerable<IDiscountRule> GetAllRules()
        {
            return _ruleRepository.GetAll();
        }
    }
}