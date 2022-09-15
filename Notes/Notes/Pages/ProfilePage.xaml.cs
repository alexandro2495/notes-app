using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Notes.Pages
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        void Create_button(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("User created");
        }
    }
}

