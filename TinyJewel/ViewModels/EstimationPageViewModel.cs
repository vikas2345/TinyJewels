// -----------------------------------------------------------------------
// <copyright file=EstimationPageViewModel company="Siemens Technology Solutions">
// Copyright © Siemens Technology Solutions All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

using TinyJewel.Common;
using TinyJewel.DependencyInjection;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace TinyJewel.ViewModels
{
    public class EstimationPageViewModel : BaseViewModel
    {
        #region Properties

        public bool IsPriviledgedUser { get; set; }

        private double _goldPrice;

        public double GoldPrice
        {
            get { return _goldPrice; }
            set
            {
                if (_goldPrice != value)
                {
                    _goldPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _goldWeight;

        public float GoldWeight
        {
            get { return _goldWeight; }
            set
            {
                if (_goldWeight != value)
                {
                    _goldWeight = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _payableAmount;

        public double PayableAmount
        {
            get { return _payableAmount; }
            set
            {
                if (_payableAmount != value)
                {
                    _payableAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _discountPercent;

        public float DiscountPercent
        {
            get { return _discountPercent; }
            set
            {
                if (_discountPercent != value)
                {
                    _discountPercent = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public string FileAbsolutePath { get; set; }

        private ICommand _calculateButtonCommand;
        public ICommand CalculateButtonCommand =>
            _calculateButtonCommand ?? (_calculateButtonCommand = new Command(() => ExecuteCalculateBtnCommand()));

        private ICommand _exitButtonCommand;
        public ICommand ExitButtonCommand =>
            _exitButtonCommand ?? (_exitButtonCommand = new Command(() => ExecuteExitBtnCommand()));

        private ICommand _printToScreenCommand;
        public ICommand PrintToScreenCommand =>
            _printToScreenCommand ?? (_printToScreenCommand = new Command(() => ExecutePrintToScreenCommand()));

        private ICommand _printToFileCommand;
        public ICommand PrintToFileCommand =>
            _printToFileCommand ?? (_printToFileCommand = new Command(() => ExecutePrintToFileCommand()));

        private ICommand _printToPaperCommand;
        public ICommand PrintToPaperCommand =>
            _printToPaperCommand ?? (_printToPaperCommand = new Command(() => ExecutePrintToPaperCommand()));

        #endregion Properties

        #region Constructors
        public EstimationPageViewModel()
        {
            FetchLoggedInUserDetail();
        }

        #endregion Constructors

        #region Public Methods
        #endregion Public Methods

        #region Private Methods

        private void FetchLoggedInUserDetail()
        {
            if (Equals(StorageContainer.Instance.UserType, UserTypes.Priviledged))
            {
                IsPriviledgedUser = true;
                DiscountPercent = 2;
                UserName = "Priviledged User";
            }
            else
            {
                UserName = "Normal User";
            }
        }

        private void ExecuteCalculateBtnCommand()
        {
            PayableAmount = (GoldPrice * GoldWeight) - (DiscountPercent * (GoldPrice * GoldWeight) / 100);
            Console.WriteLine(PayableAmount);
        }

        private async void ExecuteExitBtnCommand()
        {
            await PopPage();
        }

        private async void ExecutePrintToPaperCommand()
        {
            try
            {
                await ShareUri(FileAbsolutePath);
            }
            catch (Exception ex)
            {

            }
        }

        private async void ExecutePrintToScreenCommand()
        {
            await DisplayAlert("Payable Amount : " + PayableAmount.ToString());
        }

        private async void ExecutePrintToFileCommand()
        {
            try
            {
                var result = await VerifyPermissionStatus();
                if (result)
                {
                    string filePath = "";
                    if (Device.RuntimePlatform == Device.Android)
                        filePath = DependencyService.Get<IFileSystem>().GetFilePath() + "/TinyJewel/";

                    else
                        filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filename = Path.Combine(filePath, "TinyJewels.txt");
                    File.WriteAllText(filename, "Payable Amount : " + PayableAmount.ToString());
                    FileAbsolutePath = Path.Combine(filePath, filename);
                    await DisplayAlert("File saved to:" + Path.Combine(filePath, filename));
                }
            }
            catch (Exception ex)
            {
                await RaiseException(ex.Message);
            }

        }

        private async Task<bool> VerifyPermissionStatus()
        {
            try
            {
                var readAccess = Permissions.CheckStatusAsync<Permissions.StorageRead>();
                var writeAccess = Permissions.CheckStatusAsync<Permissions.StorageWrite>();

                if (readAccess.Status.Equals(PermissionStatus.Granted) &&
                    writeAccess.Status.Equals(PermissionStatus.Granted))
                {
                    return true;
                }

                else
                {
                    await Permissions.RequestAsync<Permissions.StorageRead>();
                    await Permissions.RequestAsync<Permissions.StorageWrite>();
                    return true;
                }

            }
            catch (PermissionException ex)
            {
                await RaiseException(ex.Message);
                return true;
            }
        }

        #endregion Private Methods

        private async Task ShareUri(string filePath)
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                File = new ShareFile(filePath),
                Title = "Share File via : "
            });
        }
    }
}
