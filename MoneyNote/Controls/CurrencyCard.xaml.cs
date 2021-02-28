
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyNote.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrencyCard : ContentView
    {
        //========================= Title ==================
        public static readonly BindableProperty TitleProperty = BindableProperty
            .Create(nameof(Title), typeof(string), typeof(CurrencyCard), default(string));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        //========================= Value ==================
        public static readonly BindableProperty ValueProperty = BindableProperty
            .Create(nameof(Value), typeof(string), typeof(CurrencyCard), default(string));

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        //========================= ColorValue ==================
        public static readonly BindableProperty ValueColorProperty = BindableProperty
            .Create(nameof(ValueColor), typeof(Color), typeof(CurrencyCard), default(Color));

        public Color ValueColor
        {
            get => (Color)GetValue(ValueColorProperty);
            set => SetValue(ValueColorProperty, value);
        }
        public CurrencyCard()
        {
            InitializeComponent();
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
            {
                titleLabel.Text = Title;
            }
            if (propertyName == ValueProperty.PropertyName)
            {
                valuelabel.Text = Value;
            }
            if (propertyName == ValueColorProperty.PropertyName)
            {
                valuelabel.TextColor = ValueColor;
            }
        }
    }
}
