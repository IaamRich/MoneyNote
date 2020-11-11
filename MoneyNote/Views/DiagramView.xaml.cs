﻿using System.Collections.Generic;
using Microcharts;
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
        List<Entry> entries = new List<Entry>
        {
            new Entry(200)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "January",
                ValueLabel = "200",
            },
            new Entry(400)
            {
                Color = SKColor.Parse("#00BFFF"),
                Label = "February",
                ValueLabel = "400"
            },
            new Entry(300)
            {
                Color = SKColor.Parse("#5f6f2e "),
                Label = "March",
                ValueLabel = "300"
            }
        };

        //Basics 2 variable
        //public bool ShowFill { get; set; }
        ////Basics 4 variable
        //SKCanvasView canvasView;
        public DiagramView()
        {
            InitializeComponent();

            //chart.Chart = new RadialGaugeChart { Entries = entries };
            //chart.Chart = new BarChart { Entries = entries };
            chart.Chart = new DonutChart
            {
                Entries = entries,
                BackgroundColor = SKColor.Parse("#ffffff"),
                LabelTextSize = 50,
                LabelColor = SKColor.Parse("#6d6a61"),
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
