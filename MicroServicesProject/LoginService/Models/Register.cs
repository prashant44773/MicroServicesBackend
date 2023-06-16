using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyCloakApi.Models
{

    public class Register
    {
            public string username { get; set; }
            public bool password { get; set; }
            public string Name { get; set; }
            public string email { get; set; }
    }
}
