using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UsersModel
    {
        public string Username { get; set; }
        public string UserAddress { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}
