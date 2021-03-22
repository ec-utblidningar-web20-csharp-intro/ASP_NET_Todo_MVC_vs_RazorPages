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
    public class CompletedModel : PageModel
    {
        private readonly TodoDbContext _context;

        public CompletedModel(TodoDbContext context)
        {
            _context = context;
        }

        public List<Todo> Todos { get;set; }
        public async Task OnGetAsync()
        {
            Todos = await _context.Todos.Where(t => !t.IsDone).ToListAsync();
        }
    }
}
