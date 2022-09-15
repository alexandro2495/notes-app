using System;
using Xamarin.Forms;

namespace Notes.Effects
{
    public class EntryOnFocusEffect : RoutingEffect
    {
        public EntryOnFocusEffect() : base($"Notes.Effects.{nameof(EntryOnFocusEffect)}")
        {
        }
    }
}