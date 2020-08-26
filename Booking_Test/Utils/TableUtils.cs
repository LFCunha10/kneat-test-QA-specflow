using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Booking_Test.Utils
{
    class TableUtils
    {
        public static Dictionary<string, string> convertTableToDictionary(Table table)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            return dictionary;
        }
    }
}
