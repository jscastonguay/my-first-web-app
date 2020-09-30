using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MyFirstWebApp.Controllers
{
    public class TodosController : Controller
    {
        [TempData]
        public int _counter { get; set; }

        [HttpPost]
        public IActionResult Index(int Counter)
        {
            Console.WriteLine($"Index(int Counter): {Counter}, {_counter}");
            _counter = Counter;
            ViewData["Counter"] = _counter;
            TempData.Keep();
            return View();
        }

        public IActionResult Index()
        {
            Console.WriteLine($"Index(): {_counter}");
            ViewData["Counter"] = _counter;
            TempData.Keep();
            return View();
        }

        public IActionResult CounterInc()
        {
            Console.WriteLine($"CounterInc(): {_counter}");
            _counter++;
            TempData.Keep();
            return RedirectToAction(nameof(Index));
        }
    }
}
