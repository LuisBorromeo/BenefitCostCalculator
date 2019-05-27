namespace EmployeeBenefits.Type.Rule
{
    public interface IRuleEvaluator
    {
        Quote GetQuote(Employee employee, string[] dependentNames);
    }
}