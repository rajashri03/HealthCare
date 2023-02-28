using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class GetDoctors
    {
        public string Doctorname { get; set; }
        public string Phonenumber { get; set; }
        public string emailaddress { get; set; }
        public string Address { get; set; }
        public int approve { get; set; }
        public int userid { get; set; }
    }
}
