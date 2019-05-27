using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using EmployeeBenefits.Host.Controllers;
using EmployeeBenefits.Type;
using EmployeeBenefits.VM;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BenefitCostCalculator.Test
{
    public class QuoteControllerTest
    {
        public QuoteControllerTest()
        {
        }

        [Fact]
        public void Should_generate_a_quote()
        {
            var quoteRequest = new BenefitsCostQuoteRequest();
            var quote = new Quote();

            var mockService = new Mock<IQuoteService>();
            mockService
                .Setup(x => x.GenerateQuote(It.IsAny<string>(), It.IsAny<string[]>()))
                .Returns(quote);

            var mockMapper = new Mock<IMapper>();

            var controller = new QuoteController(mockService.Object, mockMapper.Object);


            var actionResult = controller.Post(quoteRequest);


            mockService.Verify(es => es.GenerateQuote(It.IsAny<string>(), It.IsAny<string[]>()), Times.Once);
        }

        [Fact]
        public void Should_return_bad_request_if_quoteRequest_fails_validation()
        {
            var quoteRequest = new BenefitsCostQuoteRequest();
            var quote = new Quote();
            var mockService = new Mock<IQuoteService>();
            mockService
                .Setup(x => x.GenerateQuote(It.IsAny<string>(), It.IsAny<string[]>()))
                .Returns(quote);

            var mockMapper = new Mock<IMapper>();
            mockMapper
                .Setup(m => m.Map<BenefitsCostQuoteResult>(It.IsAny<Quote>()))
                .Returns(new BenefitsCostQuoteResult());

            var controller = new QuoteController(mockService.Object, mockMapper.Object);


            var actionResult = controller.Post(quoteRequest);


            mockService.Verify(es => es.GenerateQuote(It.IsAny<string>(), It.IsAny<string[]>()), Times.Once);
            mockMapper.Verify(m => m.Map<BenefitsCostQuoteResult>(It.IsAny<Quote>()), Times.Once);
        }
    }
}
