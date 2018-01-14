using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Drawing;
using System.ComponentModel;

namespace Community.ImageHelpers.Activities
{
    public sealed class ChangeColorOnImage : CodeActivity
    {
        public InArgument<Image> ImageToConvert { get; set; }

        [Description()]
        public InArgument<Color> ColorToChange { get; set; }

        public InArgument<Color> ColorToSet { get; set; }

        public InArgument<int> SkipPixelsFromLeft { get; set; }
        public InArgument<int> SkipPixelsFromTop { get; set; }
        public InArgument<int> SkipPixelsFromRight { get; set; }
        public InArgument<int> SkipPixelsFromBottom { get; set; }

        public OutArgument<Image> ChangedImage { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Image originalImage = context.GetValue(this.ImageToConvert);
            Color undesiredColor = context.GetValue(this.ColorToChange);
            Color desiredColor = context.GetValue(this.ColorToSet);

            // Get number of pixels to skip
            int skipLeft = context.GetValue(this.SkipPixelsFromLeft);
            int skipTop = context.GetValue(this.SkipPixelsFromTop);
            int skipRight = context.GetValue(this.SkipPixelsFromRight);
            int skipBottom = context.GetValue(this.SkipPixelsFromBottom);

            Bitmap newBitmap = new Bitmap(originalImage);

            for (int x = 0 + skipLeft; x < newBitmap.Width - skipRight; x++)
            {
                for (int y = 0 + skipBottom; y < newBitmap.Height - skipTop; y++)
                {
                    var originalColor = newBitmap.GetPixel(x, y);
                    if (StaticHelpers.ClassifyAsGeneralColor(originalColor) == undesiredColor)
                        newBitmap.SetPixel(x, y, desiredColor);
                }
            }

            ChangedImage.Set(context, newBitmap);
        }
    }
}
