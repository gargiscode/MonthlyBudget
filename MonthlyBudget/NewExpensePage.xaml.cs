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

            //string thisMonth = DateTime.Now.ToString("MMMM");
            //DatePickerBox.MinimumDate = DateTime.Now.
            //if (!string.IsNullOrEmpty(exp.FileName))
            //{

            //}
        }
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var exp = (Expense)BindingContext;
            if (string.IsNullOrEmpty(exp.FileName))
            {
                //Create a new file to write the expense
                string randomFileName;

                randomFileName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.exp.txt");

                exp.FileName = randomFileName;

                //Error Handling for user input
                if (string.IsNullOrEmpty(NameEditor.Text))
                {
                    await DisplayAlert("Error Saving Expense", "Store Name can not be empty", "OK");
                    return;
                }
                exp.Name = NameEditor.Text;

                //Convert string input to double
                double temp;
                if (!double.TryParse(AmountEditor.Text, out temp))
                {
                    await DisplayAlert("Error Saving Expense", "Amount Spent should be a decimal number", "OK");
                    return;
                }
                exp.Amount = temp;
                
                if(string.IsNullOrEmpty(CategoryPicker.SelectedItem.ToString()))
                {
                    await DisplayAlert("Error Saving Expense", "Category field can not be empty", "OK");
                    return;
                }

                // Get the corresponding image file for the selected category
                int catIndex = CategoryPicker.SelectedIndex;
                string categoryName = categoryList.ElementAt(catIndex).Name;
                exp.CategoryImage = categoryList.ElementAt(catIndex).ImageFile;

                exp.Date = DatePickerBox.Date.Date;
                

                //List<Category> categoryList = new List<Category>();
                Category.InitializeCategory(categoryList);
/*
                foreach (var cat in categoryList)
                {
                    if (categoryName == cat.Name)
                    {
                        exp.CategoryImage = cat.ImageFile;
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