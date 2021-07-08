using System;
using System.Collections.Generic;
using System.Text;

namespace MonthlyBudget.Models
{
    public class Expense
    {
        //public static decimal Budget { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string FileName { get; set; }
        public string CategoryImage { get; set; }

        //public string ImageFile { get; set; }
        //public static bool isFileLoaded { get; set; }
    }
}
