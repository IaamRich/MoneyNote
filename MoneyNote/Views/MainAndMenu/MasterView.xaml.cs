using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace MoneyNote
{
    public partial class MasterView : ReactiveMasterDetailPage<MasterViewModel>
    {
        public MasterView(MasterViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();

            AddCloseMenuFuncToMenuTitleElements();

            this.WhenActivated(
                disposables =>
                {
                    this
                        .OneWayBind(ViewModel, vm => vm.MenuItems, v => v.MyListView.ItemsSource)
                        .DisposeWith(disposables);
                    this
                        .Bind(ViewModel, vm => vm.Selected, v => v.MyListView.SelectedItem)
                        .DisposeWith(disposables);
                    this
                        .WhenAnyValue(x => x.ViewModel.Selected)
                        .Where(x => x != null)
                        .Subscribe(
                            _ =>
                            {
                                // Deselect the cell.
                                MyListView.SelectedItem = null;
                                // Hide the master panel.
                                IsPresented = false;
                            })
                        .DisposeWith(disposables);
                });
        }
        private void AddCloseMenuFuncToMenuTitleElements()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                // cast to an image
                //Image theImage = (Image)sender;
                IsPresented = false;
                // now you have a reference to the image
            };
            foreach (var item in menuTitle.Children)
            {
                item.GestureRecognizers.Add(tapGestureRecognizer);
            }
        }
    }
}
