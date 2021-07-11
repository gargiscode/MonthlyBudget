using MonthlyBudget.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace MonthlyBudget
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewExpensePage : ContentPage
    {
        List<Category> categoryList;
        public NewExpensePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var exp = (Expense)BindingContext;
            categoryList = new List<Category>();
            Category.InitializeCategory(categoryList);
            CategoryPicker.ItemsSource = categoryList;

            //if (!string.IsNullOrEmpty(exp.FileName))
            //{

            //}
        }
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var exp = (Expense)BindingContext;
            if (string.IsNullOrEmpty(exp.FileName))
            {
                //Create a new file
                string randomFileName;

                randomFileName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.exp.txt");

                exp.FileName = randomFileName;
                exp.Amount = AmountEditor.Text;
                exp.Name = NameEditor.Text;
                exp.Date = DatePickerBox.Date.Date;
                string categoryName = CategoryPicker.SelectedItem.ToString();

                //List<Category> categoryList = new List<Category>();
                Category.InitializeCategory(categoryList);

                foreach (var cat in categoryList)
                {
                    if (categoryName == cat.Name)
                    {
                        exp.CategoryImage = cat.ImageFile;
                        break;
                    }
                }

                /*
                switch(categoryIndex)
                {
                    case 0:
                        {
                            exp.Category = "Grocery"; 
                            exp.CategoryImage = "grocery.bmp";
                            break;
                        }
                    case 1:
                        {
                            exp.Category = "Utility Bills";
                            exp.CategoryImage = "utility.bmp";
                            break;
                        }
                    case 2:
                        {
                            exp.Category = "Mortgage";
                            exp.CategoryImage = "mortgage.bmp";
                            break;
                        }
                    case 3:
                        {
                            exp.Category = "Acitivities";
                            exp.CategoryImage = "activities.bmp";
                            break;
                        }
                    case 4:
                        {
                            exp.Category = "Travel";
                            exp.CategoryImage = "travel.bmp";
                            break;
                        }
                    case 5:
                        {
                            exp.Category = "Miscellaneous";
                            exp.CategoryImage = "misc.bmp";
                            break;
                        }
                }
                */

                string writeToFile = JsonConvert.SerializeObject(exp);
                File.WriteAllText(randomFileName, writeToFile);

            }
            //File.WriteAllText(note.FileName, editor.Text);
            await Navigation.PopModalAsync();
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}