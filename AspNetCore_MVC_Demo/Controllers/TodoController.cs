using AspNetCore_MVC_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore_MVC_Demo.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore_MVC_Demo.Controllers
{
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private readonly TodoDbContext _context;

        public TodoController(ILogger<TodoController> logger, TodoDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        // Hur man hittar till sin razorpage
        // Pages/Do/Shop/Buy.cshtml.cs -> /Do/Shop/Buy

        // Hur man hittar till sin MVC controller
        // Controllers/TodoController.cs
        // [Todo]Controller.[Index]() -> Todo/Index
        // [Todo]Controller.[Error]() -> Todo/Error

        public List<Todo> Todos { get; set; }
        public string Title { get; set; }
        public async Task<IActionResult> All()
        {
            Title = "All Todos";
            Todos = await _context.Todos.ToListAsync();
            return View("Todo", this);
        }
        public async Task<IActionResult> Current()
        {
            Title = "Current Todos";
            Todos = await _context.Todos.Where(t => !t.IsDone).ToListAsync();
            return View("Todo", this);
        }
        public async Task<IActionResult> Completed()
        {
            Title = "Completed Todos";
            Todos = await _context.Todos.Where(t => t.IsDone).ToListAsync();
            return View("TodoNoAdd", this);
        }
        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        [BindProperty]
        public Todo NewTodo { get; set; }
        [HttpPost]
        public async Task<IActionResult> Add()
        {
            NewTodo.CreatedOn = DateTime.Now;
            NewTodo.User = await _context.Users.FirstOrDefaultAsync();

            await _context.AddAsync(NewTodo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Complete(int? id)
        {
            if (id != null)
            {
                var todo = await _context.Todos.FindAsync(id);
                todo.EndedOn = DateTime.Now;
                todo.IsDone = true;

                _context.Update(todo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Error()
        {
            return View();
        }
    }
}
