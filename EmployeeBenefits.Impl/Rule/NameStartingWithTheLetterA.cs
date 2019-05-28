using System.Collections.Generic;
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
        private string _discountName = "10% discount when name start with the letter A";

        public NameStartingWithTheLetterA(decimal baseAnnualCostPerEmployee, decimal annualCostPerDependent, decimal discountPercent)
        {
            _baseAnnualCostPerEmployee = baseAnnualCostPerEmployee;
            _annualCostPerDependent = annualCostPerDependent;
            _discountPercent = discountPercent;
        }

        public List<RuleValue> GetDiscount(QuoteParameter quoteParameter)
        {
            //decimal totalDiscount = 0;
            var quoteItems = new List<RuleValue>();

            var employeeQuoteItem = new RuleValue()
            {
                ParameterValue = quoteParameter.EmployeeName,
                Description = _discountName,
                Cost = _baseAnnualCostPerEmployee,
                DiscountAmount = 0
            };

            // 10% discount rule
            if (quoteParameter.EmployeeName.Trim().ToLower().StartsWith("a"))
            {
                decimal discountAmount = _baseAnnualCostPerEmployee * _discountPercent;
                //totalDiscount += discountAmount;

                employeeQuoteItem.IsDiscountApplied = true;
                employeeQuoteItem.DiscountAmount = discountAmount;
                
            }
            quoteItems.Add(employeeQuoteItem);

            if (quoteParameter.Dependents != null)
            {
                foreach (string name in quoteParameter.Dependents)
                {
                    var quoteItem = new RuleValue()
                    {
                        ParameterValue = name,
                        Description = _discountName,
                        Cost = _annualCostPerDependent,
                        DiscountAmount = 0
                    };

                    if (name.Trim().ToLower().StartsWith("a"))
                    {
                        decimal discountAmount = _annualCostPerDependent * _discountPercent;
                        //totalDiscount += discountAmount;

                        quoteItem.IsDiscountApplied = true;
                        quoteItem.DiscountAmount = discountAmount;
                    }

                    quoteItems.Add(quoteItem);
                }

                //                var numberOfDependentsEligibleForDiscount = quoteParameter.Dependents
                //                    .Count(name => name.Trim().ToLower().StartsWith("a"));
                //
                //                totalDiscount += (_annualCostPerDependent * _discountPercent) * (decimal) numberOfDependentsEligibleForDiscount;
            }

            return quoteItems;
        }
    }
}