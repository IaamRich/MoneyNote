using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using I18NPortable;
using MoneyNote.Models;
using Plugin.Settings;
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
                CrossSettings.Current.AddOrUpdateValue("SelectedSpendingCategory", parameter);
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
            CategoryList = new ObservableCollection<Category>();
            Categories.GetAllSpendingCategories().ForEach(x => CategoryList.Add(new Category
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
