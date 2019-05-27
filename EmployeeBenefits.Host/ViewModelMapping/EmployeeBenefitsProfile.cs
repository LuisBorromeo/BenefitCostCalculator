using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeBenefits.Type;
using EmployeeBenefits.VM;

namespace EmployeeBenefits.Host.ViewModelMapping
{
    public class EmployeeBenefitsProfile : Profile
    {
        public EmployeeBenefitsProfile()
        {
            CreateMap<Employee, EmployeeResult>();
            CreateMap<Quote, BenefitsCostQuoteResult>();
            CreateMap<RuleValue, RuleValueResult>();
        }
    }
}
