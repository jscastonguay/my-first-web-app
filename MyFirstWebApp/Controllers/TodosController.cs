using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MyFirstWebApp.Controllers
{
    public class TodosController : Controller
    {
        [TempData]
        public int _counter { get; set; }
        // 
        // GET: /Todos/
        public IActionResult Index(int Counter)
        {
            Console.WriteLine($"Voici le counter: {Counter}");
            _counter = Counter;
            ViewData["Counter"] = _counter;
            return View();
        }

        public IActionResult CounterInc()
        {
            _counter++;
            return RedirectToAction(nameof(Index), _counter);
        }
    }
}
