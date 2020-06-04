using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MoneyNote.Pages;
using System;
using Xamarin.Forms;

namespace MoneyNote
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ContactsPage();
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
