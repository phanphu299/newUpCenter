using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            if (startDate.Year == monthStart.Year && startDate.Month > monthStart.Month)
                return new List<int>();
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => monthStart.AddDays(day))
                .Where(date => date.DayOfWeek == dow && date.Day >= startDate.Day)
                .Select(date => date.Day);
        }

        public static (int, int) TinhSubMonthSubYear(int month, int year)
        {
            int subMonth = month;
            int subYear = year;
            if (subMonth == 1)
            {
                subMonth = 12;
                subYear--;
            }
            else
            {
                subMonth--;
            }
            return (subMonth, subYear);
        }

        public static string ToTiengVietKhongDau(string str)
        {
            string strFormD = str.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strFormD.Length; i++)
            {
                System.Globalization.UnicodeCategory uc =
                System.Globalization.CharUnicodeInfo.GetUnicodeCategory(strFormD[i]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(strFormD[i]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }
    }
}
