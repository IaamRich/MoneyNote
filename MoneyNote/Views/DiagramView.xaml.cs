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
        //Basics 2 variable
        //public bool ShowFill { get; set; }
        ////Basics 4 variable
        //SKCanvasView canvasView;
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
            //{
            //    new Entry(200)
            //    {
            //        Color = SKColor.Parse("#FF1493"),
            //        Label = "January",
            //        ValueLabel = "200",
            //    },
            //    new Entry(400)
            //    {
            //        Color = SKColor.Parse("#00BFFF"),
            //        Label = "February",
            //        ValueLabel = "400"
            //    },
            //    new Entry(300)
            //    {
            //        Color = SKColor.Parse("#5f6f2e "),
            //        Label = "March",
            //        ValueLabel = "300"
            //    }
            //};
            //chart.Chart = new RadialGaugeChart { Entries = entries };
            //chart.Chart = new BarChart { Entries = entries };
            //var color = (Color)App.Current.Resources["RedAlert"];
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
            //chart.Chart = new PointChart { Entries = entries };

            //Title = "GGGGG";
            //canvasView = new SKCanvasView();
            //canvasView.PaintSurface += SKCanvasView_PaintSurface;
            //Content = canvasView;
        }

        //private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        //{
        //    ShowFill ^= true;
        //    (sender as SKCanvasView).InvalidateSurface();
        //}

        /*
        private void SKCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKCanvas canvas = e.Surface.Canvas;
            canvas.Clear();

            // =========== BASICS 2 ====================


            float strokeWidth = 50;
            float xRadius = (info.Width - strokeWidth) / 2;
            float yRadius = (info.Height - strokeWidth) / 2;

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = strokeWidth
            };
            //canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);

            canvas.DrawOval(info.Width / 2, info.Height / 2, xRadius, yRadius, paint);

            if (ShowFill)
            {
                canvas.Clear();
                paint.Color = SKColors.Blue;
                paint.StrokeWidth = 25;
                paint.Style = SKPaintStyle.StrokeAndFill;
                canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);
            }

            // =========== BASICS 4 ====================
            //SKPaint paint = new SKPaint
            //{
            //    Color = SKColors.Black,
            //    TextSize = 40
            //};

            //float fontSpacing = paint.FontSpacing;
            //float x = 20;               // left margin
            //float y = fontSpacing;      // first baseline
            //float indent = 100;

            //canvas.DrawText("SKCanvasView Height and Width:", x, y, paint);
            //y += fontSpacing;
            //canvas.DrawText(String.Format("{0:F2} x {1:F2}",
            //                              canvasView.Width, canvasView.Height),
            //                x + indent, y, paint);
            //y += fontSpacing * 2;
            //canvas.DrawText("SKCanvasView CanvasSize:", x, y, paint);
            //y += fontSpacing;
            //canvas.DrawText(canvasView.CanvasSize.ToString(), x + indent, y, paint);
            //y += fontSpacing * 2;
            //canvas.DrawText("SKImageInfo Size:", x, y, paint);
            //y += fontSpacing;
            //canvas.DrawText(info.Size.ToString(), x + indent, y, paint);
        }
        */
    }
}
