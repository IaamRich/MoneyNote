using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyNote.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Sign { get; set; }
        public string Name { get; set; }
        public ImageSource Image { get; set; }
        public ICommand Command { get; set; }
    }
}
