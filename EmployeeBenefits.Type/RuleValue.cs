namespace EmployeeBenefits.Type
{
    public class RuleValue
    {
        public string Name { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Cost { get; set; }
        public bool IsDiscountApplied { get; set; }
    }
}