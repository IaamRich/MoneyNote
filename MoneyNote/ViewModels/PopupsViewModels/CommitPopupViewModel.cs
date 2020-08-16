using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using I18NPortable;
using MoneyNote.Models;
using ReactiveUI;

namespace MoneyNote.ViewModels.PopupsViewModels
{
    public class CommitPopupViewModel : ReactiveObject
    {
        #region Variables
        public II18N Strings => I18N.Current;
        public bool IsOriginalVisible { get; set; }
        public bool IsCategoriesVisible { get; set; }
        public string ChooseButtonText { get; set; }
        public ObservableCollection<Category> CategoryList { get; set; }
        public ICommand SwitchCategoryCommand { get; set; }
        public ICommand CategoryButtonCommand { get; set; }
        #endregion
        public CommitPopupViewModel()
        {
            ChooseButtonText = Strings["choose_category"];
            IsOriginalVisible = true;
            GetCategories();
            CreateCommands();
        }
        private void CreateCommands()
        {
            SwitchCategoryCommand = ReactiveCommand.Create<int>(parameter =>
            {
                CategoryList.ToList().ForEach(x => x.IsSelected = false);
                CategoryList.ToList().FirstOrDefault(x => x.Id == parameter).IsSelected = true;
                ChooseButtonText = CategoryList.ToList().FirstOrDefault(x => x.Id == parameter).Name;
                IsCategoriesVisible = false;
                IsOriginalVisible = true;
            });
            CategoryButtonCommand = ReactiveCommand.Create(() =>
            {
                IsCategoriesVisible = true;
                IsOriginalVisible = false;
            });
        }
        private void GetCategories()
        {
            CategoryList = new ObservableCollection<Category>
            {
                new Category { Id = 0, Type = CategoryType.Market, Name = Strings["type_market"], Image = "market_shop.png", IsSelected = false},
                new Category { Id = 1, Type = CategoryType.Bar, Name = Strings["type_bar"], Image = "restaurant_bar_bistro.png", IsSelected = false},
                new Category { Id = 2, Type = CategoryType.Transport, Name = Strings["type_transport"], Image = "transport.png", IsSelected = false},
                new Category { Id = 3, Type = CategoryType.Business, Name = Strings["type_business"], Image = "business.png", IsSelected = false},
                new Category { Id = 4, Type = CategoryType.Network, Name = Strings["type_network"], Image = "network_products.png", IsSelected = false},
                new Category { Id = 5, Type = CategoryType.Entertainment, Name = Strings["type_entertainment"], Image = "entertainment.png", IsSelected = false}
            };
        }
    }
}
