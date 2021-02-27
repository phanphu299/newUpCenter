using System;
using System.Collections.Generic;
using System.Linq;

namespace Up
{
    public static class Helpers
    {
        public static IEnumerable<int> DaysInMonth(int year, int month, DayOfWeek dow)
        {
            DateTime monthStart = new DateTime(year, month, 1);
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => monthStart.AddDays(day))
                .Where(date => date.DayOfWeek == dow)
                .Select(date => date.Day);
        }

        public static IEnumerable<int> DaysInMonthWithStartDate(int year, int month, DayOfWeek dow, DateTime startDate)
        {
            DateTime monthStart = new DateTime(year, month, 1);
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => monthStart.AddDays(day))
                .Where(date => date.DayOfWeek == dow && date.Day >= startDate.Day)
                .Select(date => date.Day);
        }
    }
}
