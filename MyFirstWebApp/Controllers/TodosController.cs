using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using MyFirstWebApp.Models;
using MyFirstWebApp.Business;
using MyFirstWebApp.Context.Data;
using System.Linq;
using System.Collections.Generic;

namespace MyFirstWebApp.Controllers
{
    public class TodosController : Controller
    {
        private readonly TodoContext _context;

        public TodosController(TodoContext context)
        {
            _context = context;
        }

        // Make sure that there is one and only one item in the database and
        // then returns it.
        private Counter GetCounter()
        {
            Counter Value = new Counter { counterValue = 0 };
            IQueryable<Counter> Query = _context.Counter;

            if (Query.Any())
            {
                Counter[] c = Query.ToArray();

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
        public IActionResult Index(int newCounterValue)
        {
            Console.WriteLine($"Index(int Counter): {newCounterValue}");

            Counter current = GetCounter();
            current.counterValue = newCounterValue;
            _context.Counter.Update(current);
            _context.SaveChanges();
            ViewData["Counter"] = newCounterValue;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            TodosListModel current = new TodosListModel();
            current.counter = GetCounter();

            ViewData["Counter"] = current.counter.counterValue;

            List<Todo> List = _context.Todo.ToList<Todo>();
            current.todosList = List;

            return View(current);
        }

        public IActionResult CounterInc()
        {
            Counter current = GetCounter();
            current.counterValue++;
            _context.Counter.Update(current);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddTodo()
        {
            return View("AddTodo");
        }

        [HttpPost]
        public IActionResult AddTodo(string description, int priority, int etiquette)
        {
            Console.WriteLine($"Voici la description: {description}");
            
            Todo newTodo = new Todo();
            newTodo.description = description;
            newTodo.priority = priority;
            newTodo.etiquette = (Etiquette)etiquette;

            _context.Todo.Add(newTodo);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
