using System;
using System.Linq;
using InsightDash.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsightDash.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ApiContext _ctx;
        public OrderController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // GET api/order/pageNumber/pageSize
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _ctx.Orders.Include(order => order.Customer)
                .OrderByDescending(customer => customer.Placed);

            var page = new PaginatedResponse<Order>(data, pageIndex, pageSize);

            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet("ByState")]
        public IActionResult ByState()
        {
            var orders = _ctx.Orders.Include(order => order.Customer).ToList();

            var groupResult = orders.GroupBy(order => order.Customer.state)
                .ToList()
                .Select(grp => new 
                {
                    State = grp.Key,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(result => result.Total)
                .ToList();

            return Ok(groupResult);
        }

        [HttpGet("ByCustomer/{n}")]
        public IActionResult ByCustomer(int n)
        {
            var orders = _ctx.Orders.Include(order => order.Customer).ToList();

            var groupResult = orders.GroupBy(order => order.Customer.id)
                .ToList()
                .Select(grp => new 
                {
                    Name = _ctx.Customers.Find(grp.Key).name,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(result => result.Total)
                .Take(n)
                .ToList();

            return Ok(groupResult);
        }
        [HttpGet("GetOrder/{id}", Name = "GetOrder")]
        public IActionResult GetOrder(int id)
        {
            var orderResult =  _ctx.Orders.Include(order => order.Customer)
            .First(order => order.Id == id);

            return Ok(orderResult);
        }
    }
}