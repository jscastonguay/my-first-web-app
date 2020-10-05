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

        private int Counter
        {
            get
            {
                TodoModel Value = GetCounter();
                return Value.Counter;
            }

            set
            {
                TodoModel Value = GetCounter();
                Value.Counter = value;
                _context.Counter.Update(Value);
            }
        }

        public TodosController(TodosContext context)
        {
            _context = context;
        }

        // Make sure that there is one and only one item in the database and
        // then returns it.
        private TodoModel GetCounter()
        {
            TodoModel Value = new TodoModel { Counter = 0 };
            IQueryable<TodoModel> Query = _context.Counter;

            if (Query.Any())
            {
                TodoModel[] c = Query.ToArray();

                Value = c[0];
                for (int i = 1; i < c.Length; i++)
                {
                    _context.Counter.Remove(c[i]);
                }
            }
            else
            {
                _context.Counter.Add(Value);
            }
            _context.SaveChanges();

            return Value;
        }

        [HttpPost]
        public IActionResult Index(TodoModel NewCounter)
        {
            Console.WriteLine($"Index(int Counter): {NewCounter.Counter}");

            TodoModel current = GetCounter();
            current.Counter = NewCounter.Counter;
            _context.Counter.Update(current);
            _context.SaveChanges();
            ViewData["Counter"] = NewCounter.Counter;

            return View();
        }

        public IActionResult Index()
        {
            TodoModel current = GetCounter();
            current.Counter++;
            _context.Counter.Update(current);
            _context.SaveChanges();
            ViewData["Counter"] = current.Counter;
            return View();
        }

        public IActionResult CounterInc()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
