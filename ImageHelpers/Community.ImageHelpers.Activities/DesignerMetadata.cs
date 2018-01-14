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

            var categoryAttribute = new CategoryAttribute("Community.ImageHelpers");
            var inputCategoryAttribute = new CategoryAttribute("Input");
            var outputCategorAttribute = new CategoryAttribute("Output");
            var requiredArgumentAttribute = new RequiredArgumentAttribute();

            #region ChangeColorOnImageAttributes
            var changeColorOnImageType = typeof(ChangeColorOnImage);
            var skipBordersCategoryAttribute = new CategoryAttribute("SkipBorders");

            #region Categories
            builder.AddCustomAttributes(changeColorOnImageType, categoryAttribute);

            builder.AddCustomAttributes(changeColorOnImageType, "ImageToConvert", inputCategoryAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "ColorToChange", inputCategoryAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "ColorToSet", inputCategoryAttribute);

            builder.AddCustomAttributes(changeColorOnImageType, "SkipPixelsFromLeft", skipBordersCategoryAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "SkipPixelsFromTop", skipBordersCategoryAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "SkipPixelsFromRight", skipBordersCategoryAttribute);
            builder.AddCustomAttributes(changeColorOnImageType, "SkipPixelsFromBottom", skipBordersCategoryAttribute);

            builder.AddCustomAttributes(changeColorOnImageType, "ChangedImage", outputCategorAttribute);
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

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
