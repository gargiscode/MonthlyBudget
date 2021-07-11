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
            string thisMonth = DateTime.Now.ToString("MMMM");
            var budgetFileName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),$"{thisMonth}"+"Budget.txt");
            var files = Directory.EnumerateFiles(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "*.exp.txt");
            //decimal budgetValue = 0.0M;
            string budgetStr = string.Empty;
                     
            if (File.Exists(budgetFileName))
            {
                budgetStr = File.ReadAllText(budgetFileName);
            }
            else
            {
                File.WriteAllText(budgetFileName,BudgetEditor.Text);
            }
            

            if (string.IsNullOrEmpty(budgetStr))
            {
                //Implement later
                ExpenseListView.IsVisible = false;
                AddExpenseButton.IsVisible = false;
            }
            else
            {
                BudgetLabel.IsVisible = false;
                BudgetEditor.IsVisible = false;
                SaveBudgetButton.IsVisible = false;
                ExpenseListView.IsVisible = true;
                AddExpenseButton.IsVisible = true;
                
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

        private void OnSaveBudgetButtonClicked(object sender, EventArgs e)
        {
            string thisMonth = DateTime.Now.ToString("MMMM");
            var budgetFileName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), $"{thisMonth}" + "Budget.txt");

            if (File.Exists(budgetFileName))
            {
                File.WriteAllText(budgetFileName, BudgetEditor.Text);
            }

            BudgetEditor.IsVisible = false;
            BudgetLabel.IsVisible = false;
            SaveBudgetButton.IsVisible = false;
            ExpenseListView.IsVisible = true;

        }
    }
}
