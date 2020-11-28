using System.Collections.Generic;
using Microcharts;
using MoneyNote.Services;
using MoneyNote.ViewModels;
using ReactiveUI.XamForms;
using SkiaSharp;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;

namespace MoneyNote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiagramView : ReactiveContentPage<DiagramViewModel>
    {
        public DiagramView()
        {
            InitializeComponent();
            BindingContext = ViewModel = new DiagramViewModel(new TransactionService());
            RefreshDiagram();
        }

        private void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            RefreshDiagram();
        }
        private void RefreshDiagram()
        {
            List<Entry> entries = new List<Entry>();
            var list = ViewModel.DiagramList;
            foreach (var item in list)
            {
                float piece = (float)item.Percentage;
                entries.Add(new Entry(piece)
                {
                    Color = SKColor.Parse(item.Color)
                });
            }
            chart.Chart = new DonutChart
            {
                Entries = entries,
                BackgroundColor = SKColor.Parse("#00ec7788"),
                LabelTextSize = 20,
                LabelColor = SKColor.Parse("#ff0000"),
                LabelMode = LabelMode.None,
                IsAnimated = true,
                GraphPosition = GraphPosition.AutoFill
            };
        }
    }
}
