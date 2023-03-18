using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPracticesDemo.Application.Models.Authentication
{
    public class AuthenticationRequest
    {
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } 
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
