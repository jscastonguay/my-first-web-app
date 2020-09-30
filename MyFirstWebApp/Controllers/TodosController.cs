using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Controllers
{
    public class TodosController : Controller
    {
        [TempData]
        public int Counter { get; set; }

        [HttpPost]
        public IActionResult Index(CounterModel NewCounter)
        {
            Console.WriteLine($"Index(int Counter): {NewCounter.Counter}, {Counter}");
            Counter = NewCounter.Counter;
            ViewData["Counter"] = Counter;
            TempData.Keep();
            return View();
        }

        public IActionResult Index()
        {
            Console.WriteLine($"Index(): {Counter}");
            ViewData["Counter"] = Counter;
            TempData.Keep();
            return View();
        }

        public IActionResult CounterInc()
        {
            Console.WriteLine($"CounterInc(): {Counter}");
            Counter++;
            TempData.Keep();
            return RedirectToAction(nameof(Index));
        }
    }
}
