using ReactiveUI;
using System.Diagnostics;
using System.Windows.Input;

namespace MoneyNote.ViewModels
{
    public class CommandBindViewModel : ReactiveObject
    {
        public ICommand TestCommand { get; }

        public CommandBindViewModel()
        {
            TestCommand = ReactiveCommand.Create(() =>
            {
                Debug.WriteLine("Command Executed");
            });
        }
    }
}
