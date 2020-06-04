using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionApp.Domain.Core;
using OnionApp.Domain.Interfaces;
using ProjectTaxi.Repositories;

namespace ProjectTaxi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : Controller
    {
            IDiscountRepository repo;
            public DiscountController(IDiscountRepository r)
            {
                repo = r;
            }
            public IActionResult Index()
            {
                return View(repo.GetAll());
            }
            [HttpGet("{id}")]
            public IActionResult GetDiscount(int? id)
            {
                if (!id.HasValue)
                    return BadRequest();
                Discount discount = repo.Get(id.Value);
                if (discount == null)
                    return NotFound();
                return View(discount);
            }

            public IActionResult AddDiscount() => View();

            [HttpPost]
            public IActionResult AddDiscount(Discount discount)
            {
                if (ModelState.IsValid)
                {
                    repo.Create(discount);
                    return RedirectToAction("Index");
                }
                return View(discount);
            }
        }
}
