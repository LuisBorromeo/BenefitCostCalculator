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
}
