using System.Collections.Generic;

namespace EmployeeBenefits.Type.Rule
{
    public interface IRulesService
    {
        IEnumerable<IDiscountRule> GetAllRules();
    }
}