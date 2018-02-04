using System;
using System.Collections.Generic;
using System.Text;

namespace AppViva.Models
{
    public class EquipmentModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int StudioNo { get; set; }
        public bool IsWorking { get; set; }
        public bool IsBooked { get; set; }
        public int BicycleNo { get; set; }


    }
}
