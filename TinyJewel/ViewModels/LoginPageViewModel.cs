// -----------------------------------------------------------------------
// <copyright file=LoginPageViewModel company="Siemens Technology Solutions">
// Copyright © Siemens Technology Solutions All rights reserved.
// </copyright>

using System;
using System.Diagnostics;
using System.Windows.Input;

using TinyJewel.Common;
using TinyJewel.Pages;

using Xamarin.Forms;

namespace TinyJewel.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Properties

        public string CancelButtonText { get; set; } = "Cancel";

        public string LoginButtonText { get; set; } = "Login";

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PriviledgedUserName { get; set; } = "admin";
        public string PriviledgedUserPassword { get; set; } = "admin";

        private ICommand _loginButtonCommand;
        public ICommand LoginButtonCommand =>
            _loginButtonCommand ?? (_loginButtonCommand = new Command(() => ExecuteLoginCommand()));

        private ICommand _cancelButtonCommand;
        public ICommand CancelButtonCommand =>
            _cancelButtonCommand ?? (_cancelButtonCommand = new Command(() => ExecuteCancelCommand()));

        #endregion Properties

        #region Constructors
        public LoginPageViewModel()
        {
        }
        #endregion Constructors

        #region Public Methods
        #endregion Public Methods

        #region Private Methods

        private async void ExecuteLoginCommand()
        {
            try
            {
                if (string.Equals(UserName, PriviledgedUserName) && string.Equals(Password, PriviledgedUserPassword))
                {
                    StorageContainer.Instance.UserType = UserTypes.Priviledged;
                }

                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    await DisplayAlert("Username and Password cannot be empty");
                    return;
                }
                else
                {
                    await PushPage(new EstimationPage());
                    ExecuteCancelCommand();
                }
            }
            catch (Exception ex)
            {
                await RaiseException(ex.Message);
            }
        }

        private void ExecuteCancelCommand()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }

        #endregion Private Methods
    }
}
