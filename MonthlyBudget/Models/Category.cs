using System;
using System.Collections.Generic;
using System.Text;

namespace MonthlyBudget.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string ImageFile { get; set; }

        public static void InitializeCategory(List<Category> categoryList)
        {
            categoryList.Add(new Category { Name= "Grocery", ImageFile="grocery.bmp" });
            categoryList.Add(new Category { Name = "Utilities", ImageFile = "utility.bmp" });
            categoryList.Add(new Category { Name = "Mortgage", ImageFile = "mortgage.bmp" });
            categoryList.Add(new Category { Name = "Travel", ImageFile = "travel.bmp" }); 
            categoryList.Add(new Category { Name = "Activities", ImageFile = "activities.bmp" });
            categoryList.Add(new Category { Name = "Apparel", ImageFile = "apparel.bmp" });
            categoryList.Add(new Category { Name = "Miscellaneous", ImageFile = "misc.bmp" });
        }
    }

    
}
