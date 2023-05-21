using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BlueBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private static List<Customer> customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new Customer { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
        };

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            // Assign a unique id to the new customer
            customer.Id = customers.Count + 1;
            customers.Add(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer updatedCustomer)
        {
            var existingCustomer = customers.Find(c => c.Id == id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Email = updatedCustomer.Email;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            customers.Remove(customer);
            return NoContent();
        }
    }
}
