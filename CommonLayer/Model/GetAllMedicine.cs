using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class GetAllMedicine
    {

        public int MedicineId{ get; set; }
        public string MedicineName { get; set; }
        public string MedicineDescription { get; set; }
        public int medicinesadded_bywhom { get; set; }
    }
}
