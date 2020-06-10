﻿using MoneyNote.Models;
using MoneyNote.Persistence;
using ReactiveUI.XamForms;
using SQLite;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ReactiveContentPage<MainViewModel>
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Spend> _spendings;
        public MainView()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            //this.WhenActivated(
            //    disposables =>
            //    {
            //        this
            //            .BindCommand(ViewModel, vm => vm.NavigateToDummyPage, v => v.NavigateButton)
            //            .DisposeWith(disposables);
            //    });

        }
        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Spend>();
            var spendings = await _connection.Table<Spend>().ToListAsync();
            spendings.Reverse();
            _spendings = new ObservableCollection<Spend>(spendings);
            spendingListView.ItemsSource = _spendings;

            base.OnAppearing();
        }


        async void OnAdd(object sender, System.EventArgs e)
        {
            try
            {
                int amount = Int32.Parse(entrySpend.Text);
                if (!String.IsNullOrWhiteSpace(entrySpend.Text) && amount > 0)
                {
                    string Description = await DisplayPromptAsync("Write description:", "You can skip this...");
                    if (Description == "")
                    {
                        Description = "Not specified";
                    }
                    var spend = new Spend
                    {
                        Amount = amount,
                        WhereText = Description,
                        TransactionDate = DateTime.Now
                    };

                    await _connection.InsertAsync(spend);

                    //_spendings.Add(spend);
                    _spendings.Insert(0, spend);
                    entrySpend.Text = "";
                }
                else
                {
                    await DisplayAlert("No Amount", "Enter some value", "Ok");
                    entrySpend.Text = "";
                }
            }
            catch
            {
                await DisplayAlert("Wrong Value", "Enter some value", "Ok");
                entrySpend.Text = "";
            }
        }

        async void OnUpdate(object sender, System.EventArgs e)
        {
            var spend = _spendings[0];

            spend.WhereText += " UPDATED";

            await _connection.UpdateAsync(spend);
        }

        async void OnDelete(object sender, System.EventArgs e)
        {
            var spend = _spendings[0];
            await _connection.DeleteAsync(spend);
            _spendings.Remove(spend);
        }
    }
}
