using System;
using System.Collections.Generic;
using Microcharts;
using MoneyNote.ViewModels;
using ReactiveUI;
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

            this.WhenAnyValue(x => x.ViewModel.DiagramList).Subscribe(x => RefreshDiagram());
        }

        private void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            RefreshDiagram();
        }
        private void RefreshDiagram()
        {
            List<Entry> entries = new List<Entry>();
            foreach (var item in ViewModel.DiagramList)
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
        private void ChangeFilterClicked(object sender, System.EventArgs e)
        {
            filterPanel.IsVisible = false;
            RefreshDiagram();
        }
    }
}
