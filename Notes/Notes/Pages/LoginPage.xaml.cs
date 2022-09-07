using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Notes.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("Navigate to Profile Page");
        }
        void SignIn_Clicked(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("Navigate to Main Page");
        }
    }
}

