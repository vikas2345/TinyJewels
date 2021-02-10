// -----------------------------------------------------------------------
// <copyright file=EstimationPage company="Siemens Technology Solutions">
// Copyright © Siemens Technology Solutions All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

using TinyJewel.ViewModels;

using Xamarin.Forms;

namespace TinyJewel.Pages
{
    public partial class EstimationPage : ContentPage
    {
        EstimationPageViewModel estimationPageViewModel = null;
        public EstimationPage()
        {
            InitializeComponent();
            BindingContext = estimationPageViewModel = new EstimationPageViewModel();
        }
    }
}
