using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAdminRL
    {
        public UsersModel Registration(UsersModel users);
        public string Login(LoginModel model);
    }
}
