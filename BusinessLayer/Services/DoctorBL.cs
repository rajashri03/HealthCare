using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class DoctorBL : IDoctorBL
    {
        IAdminRL iAdminRL;
        public DoctorBL(IAdminRL iAdminRL)
        {
            this.iAdminRL = iAdminRL;
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
        }
}
