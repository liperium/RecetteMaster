using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using RecetteMaster.Data;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace RecetteMaster
{
    public partial class App : Application
    {
        static RecettesDatabase database;
        // Create the database connection as a singleton.
        public static RecettesDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new RecettesDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "recettes.db3"));
                }
                return database;
            }
        }
        
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
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