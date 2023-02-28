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
            try
            {
                return iAdminRL.Login(model);
            }
            catch (Exception)
            {

                throw;
            }
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
        public MedicineModel AddMedicine(MedicineModel medicine, long userid)
        {
            try
            {
                return iAdminRL.AddMedicine(medicine,userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<GetAllMedicine> GetAllMedicines()
        {
            try
            {
                return iAdminRL.GetAllMedicines();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<GetAllMedicine> GetAllMedicinesByid(long userid)
        {
            try
            {
                return iAdminRL.GetAllMedicinesByid(userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<GetDoctors> GetAllDoctors()
        {
            try
            {
                return iAdminRL.GetAllDoctors();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ApproveORNot(int userid, int approve)
        {
            try
            {
                return iAdminRL.ApproveORNot(userid,approve);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
