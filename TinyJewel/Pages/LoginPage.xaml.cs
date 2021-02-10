// -----------------------------------------------------------------------
// <copyright file=LoginPage company="Siemens Technology Solutions">
// Copyright © Siemens Technology Solutions All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

using TinyJewel.ViewModels;

using Xamarin.Forms;

namespace TinyJewel.Pages
{
    public partial class LoginPage : ContentPage
    {
        LoginPageViewModel loginPageViewModel = null;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = loginPageViewModel = new LoginPageViewModel();
        }
    }
}
