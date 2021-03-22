using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore_MVC_Demo.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [Required]
        public string Mission { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime EndedOn { get; set; }
        public bool IsDone { get; set; }
        [Required]
        public User User { get; set; }
    }
}
