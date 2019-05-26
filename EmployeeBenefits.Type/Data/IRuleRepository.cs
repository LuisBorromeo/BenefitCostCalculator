using System.Collections.Generic;
using EmployeeBenefits.Type.Rule;

namespace EmployeeBenefits.Type.Data
{
    public interface IRuleRepository
    {
        IEnumerable<IDiscountRule> GetAll();
    }
}