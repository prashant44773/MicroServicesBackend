using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginData.Models
{
    /*public class User
    {
        public string username = "test2072";
        public bool enabled =  true;
        public bool emailVerified = false;
        public string firstName = "test20";
        public string lastName = "Product20";
        public string  email = "test@gmail.com";
    }*/

    public class Login
    {
        public string grant_type = "password";
        public string client_id = "123";
        public string username { get; set; }
        public string password { get; set; }
        public string client_secret = "S06zn70U1uqOroEwVYrpsp4WGFauDIPw";
    }

}
