using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeBenefits.Type;
using EmployeeBenefits.Type.Rule;

namespace EmployeeBenefits.Impl
{
    public class DiscountCalculator : IRuleEvaluator
    {
        private readonly Func<string, IDiscountRule[]> _ruleAccessor;

        public DiscountCalculator(Func<string, IDiscountRule[]> ruleAccessor)
        {
            _ruleAccessor = ruleAccessor;
        }
        
        public Quote GetQuote(Employee employee, string[] dependentNames)
        {
            var quoteParameter = new QuoteParameter()
            {
                EmployeeName = employee.Name,
                Dependents = dependentNames
            };

            var rules = _ruleAccessor("All");

            var quote = new Quote()
            {
                QuoteItems = new List<RuleValue>()
            };
            
            //apply rules
            foreach (var rule in rules)
            {
                quote.QuoteItems.AddRange(rule.GetDiscount(quoteParameter));
            }

            //aggregate results
            var totalDiscountAmount = quote.QuoteItems.Sum(x => x.DiscountAmount);
            var totalAmount = quote.QuoteItems.Sum(x => x.Cost) - totalDiscountAmount;
            var costPerPaycheck = totalAmount / 26;

            quote.TotalDiscounts = totalDiscountAmount;
            quote.Total = totalAmount;
            quote.CostPerPaycheck = costPerPaycheck;
            
            return quote;
        }
    }
}