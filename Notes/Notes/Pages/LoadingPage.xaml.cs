using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Notes.Pages
{
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage(string text)
        {
            InitializeComponent();
            LoadingLabel.Text = text;
        }
    }
}
