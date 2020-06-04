using Microsoft.AspNetCore.Mvc;
using OnionApp.Domain.Core;
using OnionApp.Domain.Interfaces;
using OnionApp.Services.Interfaces;
using ProjectTaxi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTaxi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {

        IOrderRepository repo;
        public OrderController(IOrderRepository r)
        {
            repo = r;
        }
        public IActionResult Index()
        {
            return View(repo.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetOrder(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            Order order = repo.Get(id.Value);
            if (order == null)
                return NotFound();
            return View(order);
        }

        public IActionResult AddOrder() => View();

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                repo.Create(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }
    }
}
