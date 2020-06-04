using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnionApp.Domain.Core;
using OnionApp.Domain.Interfaces;
using OnionApp.Infrastructure.Data;
using ProjectTaxi.Models;
using ProjectTaxi.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTaxi.Controllers
{
    public class HomeController:Controller
    { 
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new OnionApp.Domain.Core.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private readonly UnitOfWork unit;

        public HomeController(UnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<IActionResult> Indexx(int page = 1)
        {
            int pageSize = 3;   // количество элементов на странице

            IQueryable<User> source =(IQueryable<User>)unit.Users.GetList().ToList();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Users = items
            };
            return View(viewModel);
        }
    }
}


