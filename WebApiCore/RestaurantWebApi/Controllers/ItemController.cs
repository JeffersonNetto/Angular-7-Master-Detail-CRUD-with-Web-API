using Microsoft.AspNetCore.Mvc;
using RestaurantWebApi.Models;
using System.Linq;

namespace RestaurantWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly DBModel db;

        public ItemController(DBModel db)
        {
            this.db = db;
        }

        // GET: api/Item
        public IQueryable<Item> GetItems()
        {
            return db.Items;
        }
    }
}