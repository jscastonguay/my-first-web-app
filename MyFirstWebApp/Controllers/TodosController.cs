using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MyFirstWebApp.Controllers
{
    public class TodosController : Controller
    {
        [TempData]
        public int counter { get; set;}
        // 
        // GET: /Todos/
        public IActionResult Index()
        {
            Console.WriteLine($"Voici le counter: {counter}");
            counter++;
            ViewData["Counter"] = counter;
            return View();
        }

        /*public IActionResult CounterInc() {
            return RedirectToAction(nameof(Index));
        }*/
    }
}
