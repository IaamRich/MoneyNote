using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using I18NPortable;
using MoneyNote.Models;
using Plugin.Settings;
using ReactiveUI;

namespace MoneyNote.ViewModels.PopupsViewModels
{
    public class BankTransactionsPopupViewModel : ReactiveObject
    {
        #region Variables
        public II18N Strings => I18N.Current;
        public bool IsOriginalVisible { get; set; }
        public bool IsCategoriesVisible { get; set; }
        public string ChooseButtonText { get; set; }
        public string TextFromToCard { get; set; }
        public string TextFromToCash { get; set; }
        public ObservableCollection<Category> CategoryList { get; set; }
        public ICommand SwitchCategoryCommand { get; set; }
        public ICommand CategoryButtonCommand { get; set; }
        #endregion
        public BankTransactionsPopupViewModel()
        {
            ChooseButtonText = Strings["choose_category"];
            IsOriginalVisible = true;
            TextFromToCard = Strings["card"];
            TextFromToCash = Strings["cash"];
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
                var id = CategoryList.ToList().FirstOrDefault(x => x.Id == parameter).Id;
                if (id == 0)
                {
                    TextFromToCard = Strings["from_card"];
                    TextFromToCash = Strings["from_cash"];
                }
                else
                {
                    TextFromToCard = Strings["to_card"];
                    TextFromToCash = Strings["to_cash"];
                }
                CrossSettings.Current.AddOrUpdateValue("SelectedBankCategory", parameter);
                IsCategoriesVisible = false;
                IsOriginalVisible = true;
            });
            CategoryButtonCommand = ReactiveCommand.Create(() =>
            {
                IsCategoriesVisible = true;
                IsOriginalVisible = false;
            });
        }
        public void GetCategories()
        {
            CategoryList = new ObservableCollection<Category>();
            Categories.GetAllBankCategories().ForEach(x => CategoryList.Add(new Category
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Type = x.Type,
                IsSelected = false
            }));
        }
    }
}
