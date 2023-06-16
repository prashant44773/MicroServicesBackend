using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Models
{
    public class AdminCredential
    {
        public string grant_type = "password";
        public string client_id = "admin-cli";
        public string username = "admin";
        public string password = "admin@123";
        public string client_secret = "S06zn70U1uqOroEwVYrpsp4WGFauDIPw";
    }
}
