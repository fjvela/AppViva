using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AppViva.Models
{
    public class ClassModel
    {
        public int Id { get; set; }
        public int ClassTypeId { get; set; }
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Description { get; set; }
        public string StudioDesc { get; set; }
        public int StudioNo { get; set; }
        public int IntensityId { get; set; }
        public string PtName { get; set; }
        public int TotalSpaces { get; set; }
        public int SpacesAvailable { get; set; }
        public string SpacesAvailableDesc { get; set; }
        public bool IsBooked { get; set; }
        public DateTime ClassDate { get; set; }
        public int WeekNo { get; set; }
        public bool IsInduction { get; set; }
        public bool IsSpinning { get; set; }
        public bool IsAuthorised { get; set; }
        public bool IsBookingAllowed { get; set; }
        public bool IsCancelAllowed { get; set; }
        public bool IsRestricted { get; set; }
        public bool IsInductionCompleted { get; set; }
        public int BookedCount { get; set; }
        public int InductionBookedCount { get; set; }
        public int AllowedBookings { get; set; }
        public int DaysInAdvancedBookingAllowed { get; set; }
        public int MinutesPriorToBookingAllowed { get; set; }
        public int MinutesPriorToCancelAllowed { get; set; }
        public bool DhowDelete { get; set; }
        public bool IsHoAdmin { get; set; }
        public bool MaxClassesBookedToday { get; set; }

        public bool ShowButton
        {
            get
            {
                return (IsBooked || (CanBook));
            }
        }

        public bool CanBook
        {
            get { return !IsBooked && !IsFull && ClassDateTime < GlobalSetting.MaxDateTimeBook && ClassDateTime > GlobalSetting.MinDateTimeBook; }
        }

        public bool IsFull
        {
            get { return SpacesAvailable == 0; }
        }

        public DateTime ClassDateTime
        {
            get
            {
                DateTime date = DateTime.ParseExact(StartTime, "HH:mm",
                                        CultureInfo.InvariantCulture);

                return ClassDate.Add(date.TimeOfDay);
            }
        }

        public string SpacesInfo
        {
            get
            {
                string aux = string.Join(" / ", SpacesAvailable, TotalSpaces);
                return $"{SpacesAvailableDesc} ({aux})";
            }
        }
    }
}