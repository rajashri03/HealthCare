using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAdminBL
    {
        public UsersModel Registration(UsersModel users);
        public string Login(LoginModel model);
    }
}
