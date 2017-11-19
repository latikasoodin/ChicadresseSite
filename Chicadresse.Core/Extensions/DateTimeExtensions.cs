using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDateTimeString(this DateTime date)
        {
            if (date == null)
                return string.Empty;

            return date.ToString("dddd, dd MMMM yyyy");
        }

        public static string ToDateTimeString(this DateTime? date)
        {
            if (date.HasValue == false)
                return string.Empty;

            return date.Value.ToString("dddd, dd MMMM yyyy");
        }
    }
}
