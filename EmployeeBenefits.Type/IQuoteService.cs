namespace EmployeeBenefits.Type
{
    public interface IQuoteService
    {
        Quote GenerateQuote(string employeeId, string[] dependentNames);
    }
}