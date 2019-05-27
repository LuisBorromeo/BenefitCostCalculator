using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefits.VM
{
    public class BenefitsCostQuoteRequest
    {
        public string EmployeeId { get; set; }

        public string[] DependentNames { get; set; }
    }
}
