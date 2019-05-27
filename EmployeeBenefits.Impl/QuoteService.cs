using System;
using System.Collections.Generic;
using System.Text;
using EmployeeBenefits.Type;
using EmployeeBenefits.Type.Data;
using EmployeeBenefits.Type.Rule;

namespace EmployeeBenefits.Impl
{
    public class QuoteService : IQuoteService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Quote> _quoteRepository;
        private readonly IRuleEvaluator _discountCalculator;

        public QuoteService(
            IRepository<Employee> employeeRepository, 
//            IRepository<Quote> quoteRepository,
            IRuleEvaluator discountCalculator
            )
        {
            _employeeRepository = employeeRepository;
//            _quoteRepository = quoteRepository;
            _discountCalculator = discountCalculator;
        }

        public Quote GenerateQuote(string employeeId, string[] dependentNames)
        {
            if (employeeId == null) throw new ArgumentNullException(nameof(employeeId));

            var employee = _employeeRepository.Get(employeeId) ?? throw new ArgumentNullException("_employeeRepository.Get(employeeId)");

            var quote = _discountCalculator.GetQuote(employee, dependentNames);

            return quote;
        }
    }
}
