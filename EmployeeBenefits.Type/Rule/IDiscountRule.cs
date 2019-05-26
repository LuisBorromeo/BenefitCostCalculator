namespace EmployeeBenefits.Type.Rule
{
    public interface IDiscountRule
    {
        decimal GetDiscount(Employee employee);
    }
}