// -----------------------------------------------------------------------
// <copyright file=BaseViewModel company="Siemens Technology Solutions">
// Copyright © Siemens Technology Solutions All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TinyJewel.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertiesName = "")
        {
            var handler = PropertyChanged;
            if (handler == null)
                return;
            handler(this, new PropertyChangedEventArgs(propertiesName));
        }

        public async Task PushPage(Page page, bool animate = true)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page, animate);
        }

        protected async Task PopPage(bool animate = true)
        {
            await Application.Current.MainPage.Navigation.PopAsync(animate);
        }

        protected async Task DisplayAlert(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Alert", message, "Ok");
        }

        protected async Task RaiseException(string message = "")
        {
            await Application.Current.MainPage.DisplayAlert("Error", message, "Ok");
        }

    }
}
