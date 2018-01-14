using System;
using System.Activities;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.ImageHelpers.Activities
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();

            var imageHelpersCategoryAttribute = new CategoryAttribute("Community.ImageHelpers");
            var inputCategoryAttribute = new CategoryAttribute("Input");
            var outputCategoryAttribute = new CategoryAttribute("Output");
            var requiredArgumentAttribute = new RequiredArgumentAttribute();
            var skipBordersCategoryAttribute = new CategoryAttribute("SkipBorders");

            #region ChangeColorOnImageAttributes
            var changeColorOnImageType = typeof(ChangeColorOnImage);
            
            #region Categories
            builder.AddCustomAttributes(changeColorOnImageType, imageHelpersCategoryAttribute);

            builder.AddCustomAttributes(changeColorOnImageType, "ImageToConvert", inputCategoryAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "ColorToChange", inputCategoryAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "ColorToSet", inputCategoryAttribute);

            builder.AddCustomAttributes(changeColorOnImageType, "SkipPixelsFromLeft", skipBordersCategoryAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "SkipPixelsFromTop", skipBordersCategoryAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "SkipPixelsFromRight", skipBordersCategoryAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "SkipPixelsFromBottom", skipBordersCategoryAttribute);

            builder.AddCustomAttributes(changeColorOnImageType, "ChangedImage", outputCategoryAttribute);
            #endregion
            
            #region RequiredArguments
            builder.AddCustomAttributes(changeColorOnImageType, "ImageToConvert", requiredArgumentAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "ColorToChange", requiredArgumentAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "ColorToSet", requiredArgumentAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "ChangedImage", requiredArgumentAttribute);
            #endregion

            #region ArgumentDescriptions
            builder.AddCustomAttributes(changeColorOnImageType, "ImageToConvert", new DescriptionAttribute("Original image object to change color from."));
            builder.AddCustomAttributes(changeColorOnImageType, "ColorToChange", new DescriptionAttribute("System.Drawing.Color describing color range to change."));
            builder.AddCustomAttributes(changeColorOnImageType, "ColorToSet", new DescriptionAttribute("System.Drawing.Color describing color to set instead of ColorToChange."));
            builder.AddCustomAttributes(changeColorOnImageType, "ChangedImage", new DescriptionAttribute("New System.Drawing.Image generated from ImageToConvert."));
            #endregion
            #endregion

            #region GetColorsFromImage
            var getColorsFromImageType = typeof(GetColorsFromImage);

            #region Categories
            builder.AddCustomAttributes(getColorsFromImageType, imageHelpersCategoryAttribute);

            builder.AddCustomAttributes(getColorsFromImageType, "ImageToCheck", inputCategoryAttribute);
            
            builder.AddCustomAttributes(getColorsFromImageType, "SkipPixelsFromLeft", skipBordersCategoryAttribute);
            builder.AddCustomAttributes(getColorsFromImageType, "SkipPixelsFromTop", skipBordersCategoryAttribute);
            builder.AddCustomAttributes(getColorsFromImageType, "SkipPixelsFromRight", skipBordersCategoryAttribute);
            builder.AddCustomAttributes(getColorsFromImageType, "SkipPixelsFromBottom", skipBordersCategoryAttribute);
            
            builder.AddCustomAttributes(getColorsFromImageType, "ColorsInImage", outputCategoryAttribute);
            #endregion

            #region RequiredArguments
            builder.AddCustomAttributes(getColorsFromImageType, "ImageToCheck", requiredArgumentAttribute);
            builder.AddCustomAttributes(getColorsFromImageType, "ColorsInImage", requiredArgumentAttribute);
            #endregion

            #region ArgumentDescriptions
            builder.AddCustomAttributes(getColorsFromImageType, "ImageToCheck", new DescriptionAttribute("Image to get colors from."));
            builder.AddCustomAttributes(getColorsFromImageType, "ColorsInImage", new DescriptionAttribute("Sorted list of Colors, keyed by percentage of image covered. By default sorted ascending, use .Reverse to get from most common Color first."));
            #endregion
            #endregion

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
