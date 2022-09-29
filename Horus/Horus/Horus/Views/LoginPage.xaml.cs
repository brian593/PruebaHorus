using System;
using System.Collections.Generic;
using Horus.ViewModels;
using Xamarin.Forms;

namespace Horus.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageVM(Navigation);
        }
    }
}

