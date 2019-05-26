using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeBenefits.Type;
using EmployeeBenefits.VM;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefits.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
        // GET api/employee
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeResult>> Get()
        {
            var allEmployees = _employeeService.All();

            var employeeResult = _mapper.Map<EmployeeResult[]>(allEmployees);
            return employeeResult;
        }

        // GET api/employee/<shortGuid>
        [HttpGet("{id}")]
        public ActionResult<EmployeeResult> Get(string id)
        {
            var employee = _employeeService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            return _mapper.Map <EmployeeResult>(employee);
        }

        // POST api/values
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
