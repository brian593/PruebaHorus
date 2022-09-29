using System;
using System.Collections.Generic;
using Horus.ViewModels;
using Xamarin.Forms;

namespace Horus.Views
{
    public partial class GamificationPage : ContentPage
    {
        public GamificationPage(string Token)
        {
            InitializeComponent();
            BindingContext = new GamificationPageVM(Navigation, Token);
        }
    }
}

