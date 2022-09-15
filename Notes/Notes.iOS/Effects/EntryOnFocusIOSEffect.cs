using System;
using System.ComponentModel;
using Notes.Effects;
using Notes.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ResolutionGroupName("Notes.Effects")]
[assembly:ExportEffect (typeof(EntryOnFocusIOSEffect), nameof(EntryOnFocusEffect))]
namespace Notes.iOS.Effects
{
    public class EntryOnFocusIOSEffect : PlatformEffect
    {
        UIColor backgroundColor;

        protected override void OnAttached()
        {
            try
            {
                var entry = Control as UITextField;

                if (entry != null)
                {
                    Control.BackgroundColor = backgroundColor = UIColor.FromRGBA(1, 1, 1, .5f);
                    Control.TintColor = backgroundColor;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            try
            {
                if (args.PropertyName == "IsFocused")
                {

                    if ( Control.BackgroundColor == backgroundColor)
                    {
                        Control.BackgroundColor = UIColor.FromRGBA(0, 0, 0, .1f);
                        Control.TintColor = UIColor.FromRGBA(1, 0, 0, .2f);
                    }
                    else
                    {
                        Control.BackgroundColor = backgroundColor;
                        Control.TintColor = backgroundColor;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }
    }
}

