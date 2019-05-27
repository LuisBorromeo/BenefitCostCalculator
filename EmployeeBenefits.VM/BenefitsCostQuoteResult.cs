using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefits.VM
{
    public class BenefitsCostQuoteResult
    {
        public List<RuleValueResult> QuoteItems { get; set; }
        public decimal TotalDiscounts { get; set; }
        public decimal Total { get; set; }
    }

    public class RuleValueResult
    {
        public string Name { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Cost { get; set; }
        public bool IsDiscountApplied { get; set; }
    }
}
