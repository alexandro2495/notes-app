using System;
using System.ComponentModel;
using Android.Widget;
using Notes.Droid.Effects;
using Notes.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Notes.Effects")]
[assembly: ExportEffect(typeof(EntryOnFocusDroidEffect), nameof(EntryOnFocusEffect))]
namespace Notes.Droid.Effects
{
    public class EntryOnFocusDroidEffect : PlatformEffect
    {
        Android.Graphics.Color originalBackgroundColor = Android.Graphics.Color.LightGreen;
        Android.Graphics.Color backgroundColor;

        protected override void OnAttached()
        {
            try
            {
                var entry = Control as FormsEditText;
                if(entry != null)
                {
                    backgroundColor = new Android.Graphics.Color(0, 0, 0, 0);
                    Control.SetBackgroundColor(backgroundColor);
                    
                   
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

                    if (((Android.Graphics.Drawables.ColorDrawable)Control.Background).Color == backgroundColor)
                    {

                        Control.SetBackgroundColor(originalBackgroundColor);
                    }
                    else
                    {

                        Control.SetBackgroundColor(backgroundColor);
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

