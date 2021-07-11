using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonthlyBudget
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            //MonthlyGoalPage = new MonthlyGoalPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
