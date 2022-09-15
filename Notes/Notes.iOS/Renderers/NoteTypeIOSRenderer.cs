using System;
using System.ComponentModel;
using Notes.iOS.Renderers;
using Notes.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NoteTypePicker), typeof(NoteTypeIOSRenderer))]
namespace Notes.iOS.Renderers
{
    public class NoteTypeIOSRenderer : PickerRenderer
    {
        NoteTypePicker picker = null;
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

        public NoteTypeIOSRenderer()
        {
        }

        void UpdatePickerPlaceholder()
        {
            if (picker == null)
                picker = Element as NoteTypePicker;
            if (picker.Placeholder != null)
                Control.Placeholder = picker.Placeholder;
        }
    }
}

