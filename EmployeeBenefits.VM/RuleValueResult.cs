using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefits.VM
{
    public class RuleValueResult
    {
        public string Name { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Cost { get; set; }
        public bool IsDiscountApplied { get; set; }
        public string ParameterValue { get; set; }
    }
}