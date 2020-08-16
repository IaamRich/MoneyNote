using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using I18NPortable;
using MoneyNote.Models;
using ReactiveUI;

namespace MoneyNote.ViewModels.PopupsViewModels
{
    public class AddMoneyPopupViewModel : ReactiveObject
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
        public AddMoneyPopupViewModel()
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
        public void GetCategories()
        {
            CategoryList = new ObservableCollection<Category>
            {
                new Category { Id = 0, Type = CategoryType.Salary, Name = Strings["type_salary"], Image = "salary.png", IsSelected = false},
                new Category { Id = 1, Type = CategoryType.Earnings, Name = Strings["type_earnings"], Image = "additional_earnings.png", IsSelected = false},
                new Category { Id = 2, Type = CategoryType.Gift, Name = Strings["type_gift"], Image = "gift.png", IsSelected = false},
                new Category { Id = 3, Type = CategoryType.Other, Name = Strings["type_other"], Image = "other.png", IsSelected = false}
            };
        }
    }
}
