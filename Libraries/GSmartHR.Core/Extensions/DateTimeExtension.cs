using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GSmartHR.Core.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime DefaultDate = new DateTime(2000,1,1);

        public static DateTime GetString(string time)
        {
            var hrm = time.Split(':').Select(x=>Convert.ToInt32(x)).ToList();

            var date = DefaultDate;

            date = DefaultDate.AddHours(hrm[0]);

            date = DefaultDate.AddMinutes(hrm[1]);

            return date;
        }
    }
}
