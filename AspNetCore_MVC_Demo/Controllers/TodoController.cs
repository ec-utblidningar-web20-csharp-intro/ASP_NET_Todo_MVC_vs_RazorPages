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

        public List<Todo> Todos { get; set; }
        public async Task<IActionResult> Index()
        {
            Todos = await _context.Todos.ToListAsync();
            return View(this);
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

        public IActionResult Error()
        {
            return View();
        }
    }
}
