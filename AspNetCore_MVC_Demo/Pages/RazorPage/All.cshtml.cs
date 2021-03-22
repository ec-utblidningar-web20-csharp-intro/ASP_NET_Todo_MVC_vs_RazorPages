using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspNetCore_MVC_Demo.Data;
using AspNetCore_MVC_Demo.Models;

namespace AspNetCore_MVC_Demo.Pages.RazorPage
{
    public class AllModel : PageModel
    {
        private readonly TodoDbContext _context;

        public AllModel(TodoDbContext context)
        {
            _context = context;
        }

        public List<Todo> Todos { get;set; }
        public async Task OnGetAsync(int? id)
        {
            if(id != null)
            {
                var todo = await _context.Todos.FindAsync(id);
                todo.EndedOn = DateTime.Now;
                todo.IsDone = true;

                _context.Update(todo);
                await _context.SaveChangesAsync();
            }

            Todos = await _context.Todos.ToListAsync();
        }

        [BindProperty]
        public Todo NewTodo { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            NewTodo.CreatedOn = DateTime.Now;
            NewTodo.User = await _context.Users.FirstOrDefaultAsync();

            await _context.AddAsync(NewTodo);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
