using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Drawing;
using System.ComponentModel;

namespace CleanImageActivities
{

    public sealed class ChangeColorOnImage : CodeActivity
    {
        [RequiredArgument]
        [Category("Input")]
        [Description("Original image object to change color from.")]
        public InArgument<Image> ImageToConvert { get; set; }

        [RequiredArgument]
        [Category("Input")]
        [Description("System.Drawing.Color describing color range to change.")]
        public InArgument<Color> ColorToChange { get; set; }

        [RequiredArgument]
        [Category("Input")]
        [Description("System.Drawing.Color describing color range to change ColorToChange to.")]
        public InArgument<Color> ColorToSet { get; set; }

        [Category("Skip borders")]
        public InArgument<int> SkipPixelsFromLeft { get; set; }
        [Category("Skip borders")]
        public InArgument<int> SkipPixelsFromTop { get; set; }
        [Category("Skip borders")]
        public InArgument<int> SkipPixelsFromRight { get; set; }
        [Category("Skip borders")]
        public InArgument<int> SkipPixelsFromBottom { get; set; }

        [Category("Output")]
        [Description("New System.Drawing.Image generated from ImageToConvert.")]
        public OutArgument<Image> ChangedImage { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Image originalImage = context.GetValue(this.ImageToConvert);
            Color undesiredColor = context.GetValue(this.ColorToChange);
            Color desiredColor = context.GetValue(this.ColorToSet);

            // Set number of pixels to skip
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
                    if (ClassifyAsGeneralColor(originalColor) == undesiredColor)
                        newBitmap.SetPixel(x, y, desiredColor);
                }
            }

            ChangedImage.Set(context, newBitmap);
        }

        /// <summary>
        /// Classifies given Color to general range of colors. Shamelessly nicked from https://stackoverflow.com/a/8457981 .
        /// </summary>
        /// <param name="c">Color to classify.</param>
        /// <returns>General Color.</returns>
        Color ClassifyAsGeneralColor(Color c)
        {
            float hue = c.GetHue();
            float sat = c.GetSaturation();
            float lgt = c.GetBrightness();

            if (lgt < 0.2) return Color.Black;
            if (lgt > 0.8) return Color.White;

            if (sat < 0.25) return Color.Gray;

            if (hue < 30) return Color.Red;
            if (hue < 90) return Color.Yellow;
            if (hue < 150) return Color.Green;
            if (hue < 210) return Color.Cyan;
            if (hue < 270) return Color.Blue;
            if (hue < 330) return Color.Magenta;
            return Color.Red;
        }
    }
}
