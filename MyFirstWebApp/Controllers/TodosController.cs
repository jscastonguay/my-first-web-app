using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using MyFirstWebApp.Models;
using MyFirstWebApp.Business;
using MyFirstWebApp.Context.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        private TodosListModel GetModel()
        {
            TodosListModel current = new TodosListModel();
            current.counter = GetCounter();
            current.todosList = _context.Todo.ToList<Todo>();

            return current;
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
            Console.WriteLine($"Index()");
            TodosListModel current = GetModel();
            ViewData["Counter"] = current.counter.counterValue;
            return View(current);
        }

        public IActionResult CounterInc()
        {
            Console.WriteLine($"CounterInc()");
            Counter current = GetCounter();
            current.counterValue++;
            _context.Counter.Update(current);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddTodo()
        {
            Console.WriteLine($"AddTodo()");
            return View();
        }

        [HttpPost]
        public IActionResult AddTodo(string description, int priority, int etiquette)
        {
            Console.WriteLine($"AddTodo() -> Voici la description: {description}");

            if (!String.IsNullOrEmpty(description))
            {
                Todo newTodo = new Todo();
                newTodo.description = description;
                newTodo.priority = priority;
                newTodo.etiquette = (Etiquette)etiquette;

                _context.Todo.Add(newTodo);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteTodo()
        {
            Console.WriteLine($"DeleteTodo()");
            TodosListModel current = GetModel();
            return View(current);
        }

        [HttpPost]
        public IActionResult DeleteTodo(int[] todo)
        {
            Console.WriteLine($"DeleteTodo(int[] todo)");
            foreach (int item in todo)
            {
                var itemToDelete = _context.Todo.Find(item);
                _context.Todo.RemoveRange(itemToDelete);
            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateTodo()
        {
            Console.WriteLine($"UpdateTodo()");
            TodosListModel current = GetModel();
            return View(current);
        }

        [HttpPost]
        public IActionResult UpdateTodo(Todo todo)
        {
            Console.WriteLine($"UpdateTodo(Todo todo) -> {todo.description}");

            _context.Todo.Update(todo);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // Recevoir un objet JSON par le paramêtre de FindATodo() ne fonctionne pas
        // Aucune idée pourquoi.
        /*public class test {
            public int id;
        }*/
        [HttpPost]
        public IActionResult FindATodo([FromBody] /*test*/ int id)
        {
            Console.WriteLine($"FindATodo avec id: {id}");
            Todo itemFound = _context.Todo.Find(id);
            if (itemFound == null) {
                itemFound = new Todo();
                itemFound.description = "Error";
                itemFound.etiquette = 0;
                itemFound.priority = 110;
            }
            string jsonString;
            jsonString = JsonSerializer.Serialize(itemFound);
            return Content(jsonString);
        }
    }
}
