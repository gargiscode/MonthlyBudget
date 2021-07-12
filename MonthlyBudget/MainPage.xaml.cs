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
        public double TotalBudget;
        public double AmountSpent;
        public double AmountRemaining;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var expenseList = new List<Expense>();
            // Get the current month. To be used as a prefix to the budget filename
            string thisMonth = DateTime.Now.ToString("MMMM");
            var budgetFileName = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),$"{thisMonth}"+"Budget.txt");
            var files = Directory.EnumerateFiles(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "*.exp.txt");
            
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
                    AmountSpent += ex.Amount;
                }
                //ExpenseListView.ItemsSource = expenseList.OrderBy(n => n.Date).ToList();
                ExpenseListView.ItemsSource = expenseList.ToList();

                // Calculate the remaining amount
                double.TryParse(budgetStr, out TotalBudget);
                AmountRemaining = TotalBudget - AmountSpent;

                //Set the total budget, remaining amount and amount spent for display
                TotalBudgetLabel.Text = TotalBudget.ToString();
                AmountSpentLabel.Text = AmountSpent.ToString();
                AmountRemainingLabel.Text = AmountRemaining.ToString();


                
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
