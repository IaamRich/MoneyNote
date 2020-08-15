using System.Collections.ObjectModel;
using I18NPortable;
using MoneyNote.Models;

namespace MoneyNote.ViewModels.PopupsViewModels
{
    public class CommitPopupViewModel
    {
        public string TestText { get; set; }
        public ObservableCollection<Category> CategoryList { get; set; }
        public II18N Strings => I18N.Current;
        public CommitPopupViewModel()
        {
            GetCategories();
            TestText = "GUGUGU";
        }
        private void GetCategories()
        {
            CategoryList = new ObservableCollection<Category>
            {
                new Category { Id = 0, Type = TransactionType.Market, Name = Strings["type_market"], Image = "market_shop.png", IsSelected = false},
                new Category { Id = 1, Type = TransactionType.Bar, Name = Strings["type_bar"], Image = "restaurant_bar_bistro.png", IsSelected = false},
                new Category { Id = 2, Type = TransactionType.Transport, Name = Strings["type_transport"], Image = "transport.png", IsSelected = false},
                new Category { Id = 3, Type = TransactionType.Business, Name = Strings["type_business"], Image = "business.png", IsSelected = false},
                new Category { Id = 4, Type = TransactionType.Network, Name = Strings["type_network"], Image = "network_products.png", IsSelected = false},
                new Category { Id = 5, Type = TransactionType.Entertainment, Name = Strings["type_entertainment"], Image = "entertainment.png", IsSelected = false}
            };
        }
    }
}
