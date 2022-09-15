using System;
using Notes.Data.Models;
using Xamarin.Forms;
//using Notes.Data.Models;
namespace Notes.Templates
{
    public class NoteTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MusicTemplate { get; set; }
        public DataTemplate PictureTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var note = (Note)item;
            switch (note.Type)
            {
                case NoteType.Music: return MusicTemplate;
                case NoteType.Picture: return PictureTemplate;
                default: return TextTemplate;
            }
        }
    }
}

