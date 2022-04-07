using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjAndreAirlinesWebAPI.Model
{
    public class User : Person
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
        public Role Role { get; set; }
    }
}
