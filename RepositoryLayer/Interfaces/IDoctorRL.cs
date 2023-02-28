using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IDoctorRL
    {
        public MedicineModel AddMedicine(MedicineModel users, long userid);

    }
}
