using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspNetCore_MVC_Demo.Data;
using AspNetCore_MVC_Demo.Models;

namespace AspNetCore_MVC_Demo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TodoDbContext _context;

        public IndexModel(TodoDbContext context)
        {
            _context = context;
        }

        public List<Todo> Todo { get;set; }

        public async Task OnGetAsync()
        {
            Todo = await _context.Todos.ToListAsync();
        }
    }
}
