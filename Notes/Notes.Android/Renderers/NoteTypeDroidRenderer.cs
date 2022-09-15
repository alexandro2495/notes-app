using System;
using System.ComponentModel;
using Android.Content;
using Notes.Droid.Renderers;
using Notes.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NoteTypePicker), typeof(NoteTypeDroidRenderer))]
namespace Notes.Droid.Renderers
{
    public class NoteTypeDroidRenderer : PickerRenderer
    {
        NoteTypePicker picker = null;

        public NoteTypeDroidRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                picker = Element as NoteTypePicker;
                UpdatePickerPlaceholder();
                if (picker.SelectedIndex <= 0)
                {
                    UpdatePickerPlaceholder();
                }
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (picker != null)
            {
                if (e.PropertyName.Equals(NoteTypePicker.PlaceholderProperty.PropertyName))
                {
                    UpdatePickerPlaceholder();
                }
            }
        }

        void UpdatePickerPlaceholder()
        {
            if (picker == null)
                picker = Element as NoteTypePicker;
            if (picker.Placeholder != null)
                Control.Hint = picker.Placeholder;
        }
    }
}

