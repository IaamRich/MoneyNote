using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DynamicData;
using I18NPortable;
using MoneyNote.Models;
using MoneyNote.Services;
using ReactiveUI;

namespace MoneyNote
{
    public class HistoryViewModel : ReactiveObject, IRoutableViewModel
    {
        //Used Services
        private static SpendService spendService;
        //Variables for normal functionality of the page
        public string UrlPathSegment => Strings["menu_history"];
        public IScreen HostScreen { get; }
        public II18N Strings => I18N.Current;
        //List Variables
        private SourceList<Spend> _spends = new SourceList<Spend>();
        private ReadOnlyObservableCollection<Spend> _spendingList;
        public ReadOnlyObservableCollection<Spend> SpendingList => _spendingList;
        public HistoryViewModel()
        {
            spendService = new SpendService();
            GetData();
        }
        private async void GetData()
        {
            var data = new List<Spend>();
            await Task.Run(() =>
            {
                data = spendService.GetAll().Result;
                data.Reverse();
                _spends.AddRange(data);
                _spends.Connect().Bind(out _spendingList).Subscribe();
            });
        }
    }
}
