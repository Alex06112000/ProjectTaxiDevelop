using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnionApp.Domain.Core;
using OnionApp.Domain.Interfaces;
using OnionApp.Infrastructure.Data;
using ProjectTaxi.Repositories;
using ProjectTaxi.ViewModels;

namespace ProjectTaxi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {

        ICarRepository repo;
        public CarController(ICarRepository r)
        {
            repo = r;
        }
        public IActionResult Index()
        {
            return View(repo.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetCar(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            Car car = repo.Get(id.Value);
            if (car == null)
                return NotFound();
            return View(car);
        }

        public IActionResult AddCar() => View();

        [HttpPost]
        public IActionResult AddCar(Car car)
        {
            if (ModelState.IsValid)
            {
                repo.Create(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }
    }
}
