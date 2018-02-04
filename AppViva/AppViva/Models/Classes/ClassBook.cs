using System;
using System.Collections.Generic;
using System.Text;

namespace AppViva.Models
{


    public class ClassBook
    {
        public int ContractPersonId { get; set; }
        public int ClubClassId { get; set; }
        public int? EquipmentNo { get; set; }
        public bool IsInduction { get; set; }
    }

}
