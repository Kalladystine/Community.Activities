using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Drawing;
using System.ComponentModel;

namespace Community.ImageHelpers
{
    public class StaticHelpers
    {
        /// <summary>
        /// Classifies given Color to general range of colors. Shamelessly nicked from https://stackoverflow.com/a/8457981 .
        /// </summary>
        /// <param name="c">Color to classify.</param>
        /// <returns>General Color.</returns>
        public static Color ClassifyAsGeneralColor(Color c)
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
