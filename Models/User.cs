using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace LloydStephanieRealty.Models
{
    public class User
    {
        public int ID { get; set; }
        public int Username { get; set; }
        public int Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsMailing { get; set; }
        public string Email { get; set; }
    }
}
