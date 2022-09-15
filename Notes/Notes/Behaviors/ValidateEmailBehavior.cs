using System;
using Notes.Utils;
using Xamarin.Forms;

namespace Notes.Behaviors
{
    public class ValidateEmailBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool isValid = ValidatorUtils.EmailIsValid(args.NewTextValue);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}

