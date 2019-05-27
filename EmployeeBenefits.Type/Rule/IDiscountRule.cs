using System.Collections.Generic;

namespace EmployeeBenefits.Type.Rule
{
    public interface IDiscountRule
    {
        List<RuleValue> GetDiscount(QuoteParameter quoteParameter);
    }
}