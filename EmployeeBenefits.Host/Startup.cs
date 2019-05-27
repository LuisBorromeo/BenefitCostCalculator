using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BenefitCostCalculator.Test;
using EmployeeBenefits.Impl;
using EmployeeBenefits.Impl.Encoding;
using EmployeeBenefits.Impl.Rule;
using EmployeeBenefits.Type;
using EmployeeBenefits.Type.Data;
using EmployeeBenefits.Type.Rule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmployeeBenefits.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper();
            
            var memoryRepository = new MemoryRepository<Employee>();
            SetupEmployees().ForEach(employee => memoryRepository.Save(employee.Id, employee));
            services.AddSingleton<IRepository<Employee>>(memoryRepository);

            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IQuoteService, QuoteService>();
            services.AddTransient<IRuleEvaluator, DiscountCalculator>();

            /* Rules Registration */
            services.AddTransient(r => new NameStartingWithTheLetterA(1000, 500, (decimal).1));

            services.AddTransient<Func<string, IDiscountRule[]>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "NameStartingWithTheLetterA":
                        return new[] { serviceProvider.GetService<NameStartingWithTheLetterA>() };
                    case "All":
                        return new[] { serviceProvider.GetService<NameStartingWithTheLetterA>() };
                    default:
                        throw new KeyNotFoundException(); // or maybe return null, up to you
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private List<Employee> SetupEmployees()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    Id = Guid.NewGuid().ToShortString(),
                    Name = "Robert Martin",
                    Salary = 52000,
                    Dependents = new List<string>
                    {
                        "Ava Martin",
                        "Eloise Martin"
                    }

                },
                new Employee()
                {
                    Id = Guid.NewGuid().ToShortString(),
                    Name = "Martin Fowler",
                    Salary = 52000,
                    Dependents = new List<string>
                    {
                        "Martha Fowler",
                        "Ben Fowler",
                        "Alex Fowler",
                    }
                },
                new Employee()
                {
                    Id = Guid.NewGuid().ToShortString(),
                    Name = "Eric Evans",
                    Salary = 52000,
                    Dependents = new List<string>
                    {
                        "Lisa Evans"
                    }
                },
                new Employee()
                {
                    Id = Guid.NewGuid().ToShortString(),
                    Name = "Alan Turing",
                    Salary = 52000
                }
            };
        }

    }
}
