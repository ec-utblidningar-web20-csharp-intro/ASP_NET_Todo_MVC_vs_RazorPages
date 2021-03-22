using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore_MVC_Demo.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(24)]
        [Required]
        public string Username { get; set; }
        [Required]
        [RegularExpression("/^w+[+.w-]*@([w-]+.)*w+[w-]*.([a-z]{2,4}|d+)$/i")]
        public string Email { get; set; }
    }
}
