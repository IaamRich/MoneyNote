//
//  Copyright 2020  
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System.Collections.Generic;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Xamarin.Forms;

namespace MoneyNote.MarkupExtensions
{
    public class TintedCachedImage : CachedImage
    {
        public static BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color),
            typeof(TintedCachedImage), Color.Transparent, propertyChanged: UpdateColor);

        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        private static void UpdateColor(BindableObject bindable, object oldColor, object newColor)
        {
            var oldcolor = (Color)oldColor;
            var newcolor = (Color)newColor;

            if (oldcolor.Equals(newcolor)) return;
            var view = (TintedCachedImage)bindable;
            var transformations = new List<ITransformation>
            {
                new TintTransformation((int) (newcolor.R * 255), (int) (newcolor.G * 255), (int) (newcolor.B * 255),
                    (int) (newcolor.A * 255))
                {
                    EnableSolidColor = true
                }
            };
            view.Transformations = transformations;
        }
    }
}
