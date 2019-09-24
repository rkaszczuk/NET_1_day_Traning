using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class DateTimeHelper
    {
        public static bool IsWeekend(this DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
