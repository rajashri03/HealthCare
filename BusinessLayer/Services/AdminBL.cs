using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        IAdminRL iAdminRL;
        public AdminBL(IAdminRL iAdminRL)
        {
            this.iAdminRL = iAdminRL;
        }
        public string Login(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public UsersModel Registration(UsersModel users)
        {
            try
            {
                return iAdminRL.Registration(users);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
