using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DBModel db;

        public OrderController(DBModel db)
        {
            this.db = db;
        }

        // GET: api/Order
        public Object GetOrders()
        {
            var result = (from a in db.Orders
                          join b in db.Customers on a.CustomerID equals b.CustomerID

                          select new
                          {
                              a.OrderID,
                              a.OrderNo,
                              Customer = b.Name,
                              a.PMethod,
                              a.GTotal
                          }).ToList();

            return result;
        }

        // GET: api/Order/5
        //[ResponseType(typeof(Order))]
        [HttpGet("{id}")]
        public IActionResult GetOrder(long id)
        {
            var order = (from a in db.Orders
                         where a.OrderID == id

                         select new
                         {
                             a.OrderID,
                             a.OrderNo,
                             a.CustomerID,
                             a.PMethod,
                             a.GTotal,
                             DeletedOrderItemIDs = ""
                         }).FirstOrDefault();

            var orderDetails = (from a in db.OrderItems
                                join b in db.Items on a.ItemID equals b.ItemID
                                where a.OrderID == id

                                select new
                                {
                                    a.OrderID,
                                    a.OrderItemID,
                                    a.ItemID,
                                    ItemName = b.Name,
                                    b.Price,
                                    a.Quantity,
                                    Total = a.Quantity * b.Price
                                }).ToList();

            return Ok(new { order, orderDetails });
        }

        // POST: api/Order
        //[ResponseType(typeof(Order))]
        public IActionResult PostOrder(Order order)
        {
            try
            {
                //Order table
                if (order.OrderID == 0)
                    db.Orders.Add(order);
                else
                    db.Entry(order).State = EntityState.Modified;

                //OrderItems table
                foreach (var item in order.OrderItems)
                {
                    if (item.OrderItemID == 0)
                        db.OrderItems.Add(item);
                    else
                        db.Entry(item).State = EntityState.Modified;
                }

                //Delete for OrderItems
                foreach (var id in order.DeletedOrderItemIDs.Split(',').Where(x => x != ""))
                {
                    OrderItem x = db.OrderItems.Find(Convert.ToInt64(id));
                    db.OrderItems.Remove(x);
                }


                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // DELETE: api/Order/5
        //[ResponseType(typeof(Order))]
        public IActionResult DeleteOrder(long id)
        {
            Order order = db.Orders.Include(y => y.OrderItems)
                .SingleOrDefault(x => x.OrderID == id);

            foreach (var item in order.OrderItems.ToList())
            {
                db.OrderItems.Remove(item);
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }        

        private bool OrderExists(long id)
        {
            return db.Orders.Count(e => e.OrderID == id) > 0;
        }
    }
}