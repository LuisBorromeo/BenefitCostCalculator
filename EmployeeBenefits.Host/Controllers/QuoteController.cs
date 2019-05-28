using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeBenefits.Type;
using EmployeeBenefits.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefits.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        private readonly IMapper _mapper;

        public QuoteController(IQuoteService quoteService, IMapper mapper)
        {
            _quoteService = quoteService;
            _mapper = mapper;
        }
        
        // POST: api/Quote
        [HttpPost]
        [ProducesResponseType(typeof(BenefitsCostQuoteResult), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] BenefitsCostQuoteRequest quoteRequest)
        {
            if (quoteRequest?.EmployeeId == null || quoteRequest.EmployeeId == string.Empty)
            {
                return BadRequest();
            }

            var quote = _quoteService.GenerateQuote(quoteRequest.EmployeeId, quoteRequest.DependentNames);

            var benefitsCostQuoteResult = _mapper.Map<BenefitsCostQuoteResult>(quote);
            return CreatedAtAction("Get", "Employee", new { id = quoteRequest.EmployeeId }, benefitsCostQuoteResult);
        }
    }
}
