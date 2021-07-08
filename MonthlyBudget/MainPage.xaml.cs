using MonthlyBudget.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace MonthlyBudget
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var expenseList = new List<Expense>();

            var budgetFileName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),"JulyBudget.txt");
            var files = Directory.EnumerateFiles(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "*.exp.txt");
            //decimal budgetValue = 0.0M;
            string budgetStr = string.Empty;

            /*
            if (File.Exists(budgetFileName))
            {
                budgetStr = File.ReadAllText(budgetFileName);
            }
            else
            {
                File.Create(budgetFileName);
                File.WriteAllText(budgetFileName,"500");
            }
            */
            budgetStr = "500";
            if (string.IsNullOrEmpty(budgetStr))
            { 
                //Implement later

            }
            else
            {
                foreach (var filename in files)
                {
                    string readStr = File.ReadAllText(filename);
                    Expense ex = JsonConvert.DeserializeObject<Expense>(readStr);
                    expenseList.Add(ex);
                }
                //ExpenseListView.ItemsSource = expenseList.OrderBy(n => n.Date).ToList();
                ExpenseListView.ItemsSource = expenseList.ToList();
            }
        }
        private async void OnAddExpenseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NewExpensePage
            {
                BindingContext = new Expense()
            });
        }

        private void OnExpenseItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}
