using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CT.Helpers
{
    public static class Extensions
    {
        public static string ToCsv(
            this IEnumerable data,
            bool headerRow = false)
        {
            using (var writer = new StringWriter())
            {
                Helper.WriteCSV(data, writer, headerRow);

                return writer.ToString();
            }
        }
    }
}