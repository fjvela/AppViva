using System;
using System.Collections.Generic;
using System.Text;

namespace AppViva.Models
{
    public class ClassWeeklyModel
    {
        public int Day { get; set; }
        public DateTime Date { get; set; }
        public IList<ClassModel> ClubClasses { get; set; }
    }
}
