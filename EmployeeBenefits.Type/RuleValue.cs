﻿namespace EmployeeBenefits.Type
{
    public class RuleValue
    {
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Cost { get; set; }
        public bool IsDiscountApplied { get; set; }
        public string ParameterValue { get; set; }
    }
}