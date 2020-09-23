using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyNote.Controls
{
    public partial class AccountSwitchFrame : ContentView
    {
        public AccountSwitchFrame()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                // more info about animations:
                // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/animation/simple

                // scale the frame to x1.2
                var scaleUpAnimationTask = this.ScaleTo(1.2, 500);
                // set opacity to 0 (transparent)
                var fadeOutAnimationTask = this.FadeTo(0, 500);

                // wait for the 2 animations to finish...
                await Task.WhenAll(scaleUpAnimationTask, fadeOutAnimationTask);

                // scale the frame back to original size
                var scaleDownAnimationTask = this.ScaleTo(1, 500);
                // set opacity back to 1 (solid)
                var fadeInAnimationTask = this.FadeTo(1, 500);

                // wait for the 2 animations to finish...
                await Task.WhenAll(scaleDownAnimationTask, fadeInAnimationTask);
            };
            this.GestureRecognizers.Add(tapGestureRecognizer);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                label.Text = Text;
            }
            if (propertyName == MoneyProperty.PropertyName)
            {
                money.Text = Money;
            }
            if (propertyName == MoneyColorProperty.PropertyName)
            {
                money.TextColor = MoneyColor;
            }
            if (propertyName == CommandProperty.PropertyName)
            {
                this.Command = Command;
            }
        }
        //========================= Label ==================
        public static readonly BindableProperty TextProperty = BindableProperty
            .Create(nameof(Text), typeof(string), typeof(AccountSwitchFrame), default(string));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        //========================= Money ==================
        public static readonly BindableProperty MoneyProperty = BindableProperty
            .Create(nameof(Money), typeof(string), typeof(AccountSwitchFrame), default(string), BindingMode.TwoWay);

        public string Money
        {
            get => (string)GetValue(MoneyProperty);
            set => SetValue(MoneyProperty, value);
        }
        //========================= MoneyColor ==================
        public static readonly BindableProperty MoneyColorProperty = BindableProperty
            .Create(nameof(MoneyColor), typeof(Color), typeof(AccountSwitchFrame), default(Color));

        public Color MoneyColor
        {
            get => (Color)GetValue(MoneyColorProperty);
            set => SetValue(MoneyColorProperty, value);
        }
        //========================= Command ==================
        public static readonly BindableProperty CommandProperty = BindableProperty
            .Create(nameof(Command), typeof(ICommand), typeof(AccountSwitchFrame), default(ICommand));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}
