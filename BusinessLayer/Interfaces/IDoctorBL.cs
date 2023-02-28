using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IDoctorBL
    {
        public MedicineModel AddMedicine(MedicineModel medicine, long userid);
        
        }
}
