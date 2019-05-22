using Microsoft.AspNetCore.Mvc;
using RestaurantWebApi.Models;
using System.Linq;

namespace RestaurantWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DBModel db;

        public CustomerController(DBModel db)
        {
            this.db = db;
        }

        // GET: api/Customer
        public IQueryable<Customer> GetCustomers()
        {
            return db.Customers;
        }       
    }
}