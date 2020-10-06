using I18NPortable;
using MoneyNote.Services.Contracts;
using ReactiveUI;
using Splat;

namespace MoneyNote.ViewModels
{
    public class DiagramViewModel : ReactiveObject, IRoutableViewModel
    {
        //Used Services
        private static IMoneyService _moneyService;
        private static ISettingsService _settingsService;
        private static ITransactionService _transactionService;
        //Variables for normal functionality of the page
        public string UrlPathSegment => Strings["menu_diagram"];
        public II18N Strings => I18N.Current;
        public IScreen HostScreen { get; }
        public DiagramViewModel(ITransactionService transactionService, IMoneyService moneyService, ISettingsService settingsService, IScreen screen = null)
        {
            _transactionService = transactionService;
            _moneyService = moneyService;
            _settingsService = settingsService;
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        }
    }
}
