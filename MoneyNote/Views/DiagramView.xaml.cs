using System;
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
            List<Entry> entries = new List<Entry>();
            BindingContext = ViewModel = new DiagramViewModel(new TransactionService());
            var list = ViewModel.DiagramList;
            foreach (var item in list)
            {
                float piece = (float)item.Percentage;
                var random = new Random();
                var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
                entries.Add(new Entry(piece)
                {
                    Color = SKColor.Parse(color),
                    Label = item.Name,
                    ValueLabel = String.Format("{0:0.00}%", item.Percentage)
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
