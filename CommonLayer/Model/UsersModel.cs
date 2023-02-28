using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CommonLayer.Model
{
    public class UsersModel
    {
        public string username { get; set; }
        public string useraddress { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string usertype { get; set; }
        public int approve { get; set; }
    }
}
