using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Scores
{
    public partial class App : Application
    {
        public static Player [] ArrayPlayer;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            ArrayPlayer = new Player[] { };
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
