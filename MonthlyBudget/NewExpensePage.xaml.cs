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
        public NewExpensePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var exp = (Expense)BindingContext;
            
            if (!string.IsNullOrEmpty(exp.FileName))
            {
                
            }
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
                    $"{Path.GetRandomFileName()}.exp.txt"
                    );
                exp.FileName = randomFileName;
                exp.Amount = AmountEditor.Text;
                exp.Name = NameEditor.Text;
                exp.Date = File.GetCreationTime(randomFileName);
                int categoryIndex = CategoryPicker.SelectedIndex;
                switch(categoryIndex)
                {
                    case 0:
                        {
                            exp.Category = "Grocery"; 
                            exp.CategoryImage = "/Assets/grocery.png";
                            break;
                        }
                    case 1:
                        {
                            exp.Category = "Utilities";
                            exp.CategoryImage = "/Assets/utilities.png";
                            break;
                        }
                    case 2:
                        {
                            exp.Category = "Travel";
                            exp.CategoryImage = "/Assets/travel.png";
                            break;
                        }
                    case 3:
                        {
                            exp.Category = "Classes";
                            exp.CategoryImage = "/Assets/classes.png";
                            break;
                        }
                }

                //File.Create(exp.FileName);
                string writeToFile = JsonConvert.SerializeObject(exp);
                File.WriteAllText(randomFileName, writeToFile);

            }
            //File.WriteAllText(note.FileName, editor.Text);
            await Navigation.PopModalAsync();
        }

        private void OnCancelButtonClicked(object sender, EventArgs e)
        {

        }
    }
}