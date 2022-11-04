//using NuGet.Frameworks;

using AnimalCountinDatabase;
using AnimalCountinDatabase.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Mvc;



namespace AnimalCounginDatabase.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task CustomerIntegrationTest()
        {
            //Create Db contest
            var factory = new CustomerContextFactory();
            using var context = factory.CreateDbContext(null);


            //Delete all existing customer
            context.Customers!.RemoveRange(await context.Customers.ToArrayAsync());
            await context.SaveChangesAsync();

            //Create customer controller
            var controller = new CustomersController(context);

            //Add customer
            var customer = new Customer() { Name = "prova" };
            await controller.Add(customer);

            //Check does the GetAll return the added customers 
            var result = (await controller.GetAll()).ToArray();

            Assert.Single(result);
            Assert.Equal("prova", result[0].Name);

        }
    }

    class CustomerContextFactory : IDesignTimeDbContextFactory<CustomerContext>
    {
        public CustomerContext CreateDbContext(string[]? args = null)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();
            optionsBuilder
                .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            return new CustomerContext(optionsBuilder.Options);
        }
    }
    
}


