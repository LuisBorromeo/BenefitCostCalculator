using System.Collections.Generic;

namespace EmployeeBenefits.Type
{
    public class Quote
    {
        public List<RuleValue> QuoteItems { get; set; }
        public decimal TotalDiscounts { get; set; }
        public decimal Total { get; set; }
    }
}