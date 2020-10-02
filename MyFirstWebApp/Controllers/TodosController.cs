using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using MyFirstWebApp.Models;
using Todos.Data;
using System.Linq;

namespace MyFirstWebApp.Controllers
{
    public class TodosController : Controller
    {
        private readonly TodosContext _context;

        public TodosController(TodosContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Index(CounterModel NewCounter)
        {
            Console.WriteLine($"Index(int Counter): {NewCounter.Counter}");

            IQueryable<int> Query = from m in _context.Counter
                                    select m.Counter;

            if (Query.Any())
            {
                // Ne fonctionne pas pour l'intsant: on doit récupéré la row pour l'update.
                _context.Counter.Update(NewCounter);
            }
            else
            {
                _context.Counter.Add(NewCounter);
            }
            _context.SaveChanges();
            ViewData["Counter"] = NewCounter.Counter;

            return View();
        }

        public IActionResult Index()
        {
            /*Console.WriteLine($"Index(): {Counter}");
            ViewData["Counter"] = Counter;
            TempData.Keep();*/
            ViewData["Counter"] = GetCounterValue();

            return View();
        }

        public IActionResult CounterInc()
        {
            //Console.WriteLine($"CounterInc(): {Counter}");
            //Counter++;
            //TempData.Keep();
            //var list = _context.

            return RedirectToAction(nameof(Index));
        }

        private int GetCounterValue()
        {
            int Value = 0;

            // Récupère tous les éléments (Counter) dans la BD.
            IQueryable<int> Query = from m in _context.Counter
                                    select m.Counter;

            if (Query.Any())
            {
                Value = Query.First();
            }
            else
            {
                _context.Counter.Add(new CounterModel { Counter = Value });
                _context.SaveChanges();
            }

            return Value;
        }
    }
}
