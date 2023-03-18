using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPracticesDemo.Application.Models.Authentication
{
    public class JwtSettings
    {
        public string Key { get; set; } 
        public string Issuer { get; set; } 
        public string Audience { get; set; } 
        public int DurationInMinutes { get; set; } 
    }
}
