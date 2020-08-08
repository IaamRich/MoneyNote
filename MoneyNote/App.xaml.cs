using System;
using System.Diagnostics;
using System.Reflection;
using I18NPortable;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MoneyNote.Persistence;
using MoneyNote.Resources;
using SQLite;
using Xamarin.Forms;

namespace MoneyNote
{
    public partial class App : Application
    {
        public static SQLiteAsyncConnection Database;

        public App()
        {
            InitializeComponent();
            Database = DependencyService.Get<ISQLiteDb>().GetConnection();
            MainPage = new NavigationPage(new SplashPage());
            XF.Material.Forms.Material.Init(this);
            //var bootstrapper = new AppBootstrapper();
            //MainPage = new MasterView(bootstrapper.CreateMasterViewModel());
        }
        protected override void OnStart()
        {
            AppCenter.Start("android=f38a5d72-667c-4eeb-8a2c-1c534ccd9b3e;" +
                              "uwp={Your UWP App secret here};" +
                              "ios=e5753842-cc10-4a5d-b170-dac37070ad4b;",
                              typeof(Analytics), typeof(Crashes));

            I18N.Current
                .SetNotFoundSymbol("$") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
                .SetFallbackLocale("en") // Optional but recommended: locale to load in case the system locale is not supported
                .SetThrowWhenKeyNotFound(true) // Optional: Throw an exception when keys are not found (recommended only for debugging)
                .SetLogger(text => Debug.WriteLine(text)) // action to output traces
                                                          //.SetResourcesFolder("OtherLocales") // Optional: The directory containing the resource files (defaults to "Locales")
                .Init(GetType().GetTypeInfo().Assembly); // assembly where locales live

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
