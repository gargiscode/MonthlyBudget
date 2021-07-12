using System;
using System.Collections.Generic;
using System.Text;

namespace MonthlyBudget.Models
{
    public class Expense
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string FileName { get; set; }
        public string CategoryImage { get; set; }

    }
}
