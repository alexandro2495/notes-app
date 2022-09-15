using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.CustomMarkupXtension
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceX : IMarkupExtension
    {
        public string Source { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }
            var imageSource = ImageSource.FromResource(Source, typeof(ImageResourceX).GetTypeInfo().Assembly);
            return imageSource;
        }
    }
}

