using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BenefitCostCalculator.Test;
using EmployeeBenefits.Impl.Encoding;
using EmployeeBenefits.Type;
using EmployeeBenefits.Type.Data;
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(
                options => options.WithOrigins().AllowAnyMethod()
            );

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
