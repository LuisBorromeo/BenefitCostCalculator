namespace EmployeeBenefits.Type.Rule
{
    public interface IRuleEvaluator
    {
        decimal GetTotalDiscount(Employee employee);
    }
}