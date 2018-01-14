using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Drawing;
using System.ComponentModel;

namespace Community.ImageHelpers.Activities
{
    public sealed class GetColorsFromImage : CodeActivity
    {
        public InArgument<Image> ImageToCheck { get; set; }

        public InArgument<int> SkipPixelsFromLeft { get; set; }
        public InArgument<int> SkipPixelsFromTop { get; set; }
        public InArgument<int> SkipPixelsFromRight { get; set; }
        public InArgument<int> SkipPixelsFromBottom { get; set; }

        public OutArgument<SortedList<double, Color>> ColorsInImage { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Bitmap originalImage = (Bitmap)context.GetValue(this.ImageToCheck);
            // Get number of pixels to skip
            int skipLeft = context.GetValue(this.SkipPixelsFromLeft);
            int skipTop = context.GetValue(this.SkipPixelsFromTop);
            int skipRight = context.GetValue(this.SkipPixelsFromRight);
            int skipBottom = context.GetValue(this.SkipPixelsFromBottom);

            var foundColors = new Dictionary<Color, double>();
            long totalScannedPixels = 0;
            for (int x = 0 + skipLeft; x < originalImage.Width - skipRight; x++)
            {
                for (int y = 0 + skipBottom; y < originalImage.Height - skipTop; y++)
                {
                    var originalColor = StaticHelpers.ClassifyAsGeneralColor(originalImage.GetPixel(x, y));
                    if (foundColors.ContainsKey(originalColor))
                    {
                        foundColors[originalColor] += 1;
                    }
                    else
                    {
                        foundColors[originalColor] = 1;
                    }
                    
                    totalScannedPixels += 1;
                }
            }

            var sortedFoundColors = new SortedList<double, Color>(foundColors.Count());

            foreach (var color in foundColors.Keys)
            {
                sortedFoundColors.Add(foundColors[color] / totalScannedPixels, color);
            }

            ColorsInImage.Set(context, sortedFoundColors);
        }
    }
}
