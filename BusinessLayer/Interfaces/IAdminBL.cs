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
        public MedicineModel AddMedicine(MedicineModel medicine,long userid);
        public IEnumerable<GetAllMedicine> GetAllMedicines();
        public IEnumerable<GetAllMedicine> GetAllMedicinesByid(long userid);
        public IEnumerable<GetDoctors> GetAllDoctors();
        public bool ApproveORNot(int userid,int approve);


    }
}
