using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MoneyNote.Resources;
using MoneyNote.Services;
using System;
using System.IO;
using Xamarin.Forms;

namespace MoneyNote
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "money.db";
        public static MoneyService database;
        public static MoneyService Database
        {
            get
            {
                if (database == null)
                {
                    database = new MoneyService(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            //var bootstrapper = new AppBootstrapper();
            MainPage = new NavigationPage(new SplashPage());//MasterView(bootstrapper.CreateMasterViewModel());
        }
        protected override void OnStart()
        {
            AppCenter.Start("android=f38a5d72-667c-4eeb-8a2c-1c534ccd9b3e;" +
                              "uwp={Your UWP App secret here};" +
                              "ios=e5753842-cc10-4a5d-b170-dac37070ad4b;",
                              typeof(Analytics), typeof(Crashes));

            Crashes.SentErrorReport += (sender, e) => { Console.WriteLine(e); };
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
