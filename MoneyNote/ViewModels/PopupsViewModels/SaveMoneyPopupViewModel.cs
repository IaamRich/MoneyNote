using I18NPortable;
using ReactiveUI;

namespace MoneyNote.ViewModels.PopupsViewModels
{
    public class SaveMoneyPopupViewModel : ReactiveObject
    {
        #region Variables
        public II18N Strings => I18N.Current;
        #endregion
        public SaveMoneyPopupViewModel()
        {

        }
    }
}
