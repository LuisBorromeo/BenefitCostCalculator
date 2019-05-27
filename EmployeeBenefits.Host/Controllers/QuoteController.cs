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

        // GET: api/Quote
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Quote/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            return "value";
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
            return CreatedAtAction(nameof(Get), new { id = quoteRequest.EmployeeId }, benefitsCostQuoteResult);
        }

        // PUT: api/Quote/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
