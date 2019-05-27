using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using EmployeeBenefits.Host.Controllers;
using EmployeeBenefits.Impl;
using EmployeeBenefits.Impl.Rule;
using EmployeeBenefits.Type;
using EmployeeBenefits.Type.Data;
using EmployeeBenefits.Type.Rule;
using EmployeeBenefits.VM;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BenefitCostCalculator.Test
{
    public class EmployeeControllerTest
    {
        public EmployeeControllerTest()
        {
        }

        [Fact]
        public void Should_get_a_404_when_employee_does_not_exist()
        {
            var mockService = new Mock<IEmployeeService>();
            mockService
                .Setup(x => x.Get(It.IsAny<string>()))
                .Returns((Employee)null);

            var mockMapper = new Mock<IMapper>();
            
            var employeeController = new EmployeeController(mockService.Object, mockMapper.Object);


            var actionResult = employeeController.Get("employeeId");


            mockService.Verify(es => es.Get(It.IsAny<string>()), Times.Once);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void Should_get_an_employee_from_service()
        {
            var employee = new Employee { Name = "bob" };
            var mockService = new Mock<IEmployeeService>();
            mockService
                .Setup(x => x.Get(It.IsAny<string>()))
                .Returns(employee);

            var mockMapper = new Mock<IMapper>();
            mockMapper
                .Setup(m => m.Map<EmployeeResult>(It.IsAny<Employee>()))
                .Returns(new EmployeeResult { Name = employee.Name });

            var employeeController = new EmployeeController(mockService.Object, mockMapper.Object);
            

            var actionResult = employeeController.Get("employeeId");


            mockService.Verify(es => es.Get(It.IsAny<string>()), Times.Once);
            mockMapper.Verify(m => m.Map<EmployeeResult>(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public void Should_get_employees_from_service()
        {
            var employees = new Employee[]{new Employee { Name = "bob" }};

            var mockService = new Mock<IEmployeeService>();
            mockService
                .Setup(x => x.All())
                .Returns(employees);

            var mockMapper = new Mock<IMapper>();
            mockMapper
                .Setup(m => m.Map<EmployeeResult[]>(It.IsAny<Employee[]>()))
                .Returns(new EmployeeResult[] { new EmployeeResult() { Name = "bob" } });

            var employeeController = new EmployeeController(mockService.Object, mockMapper.Object);


            var actionResult = employeeController.Get();


            mockService.Verify(es => es.All(), Times.Once);
            mockMapper.Verify(m => m.Map<EmployeeResult[]>(It.IsAny<Employee[]>()), Times.Once);
        }
    }
}
