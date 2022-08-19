using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TradingApp.ViewModels
{
    public class AddNewTrackViewModel : ViewModelBase
    {
        #region Fields
        private string message;
        private string name;
        private bool messageIsVisible;
        #endregion

        public AddNewTrackViewModel()
        {
            BackCommand = new AsyncCommand(OnBackButtonClicked);
            SavedCommand = new AsyncCommand(OnSaveButtonClicked);
        }

        #region Properties
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public bool MessageIsVisible
        {
            get => messageIsVisible;
            set => SetProperty(ref messageIsVisible, value);
        }
        #endregion

        #region Commands
        public ICommand BackCommand { get; }
        public ICommand SavedCommand { get; }
        #endregion

        #region Methods

        private async Task OnBackButtonClicked()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnSaveButtonClicked()
        {
            try
            {
                MessageIsVisible = false;

                if (Connectivity.NetworkAccess == NetworkAccess.None)
                {
                    MessageIsVisible = true;
                    Message = "No internet, please check your connection";
                    return;
                }

                if (string.IsNullOrWhiteSpace(Name))
                {
                    Message = "Name cannot be empty.";
                    MessageIsVisible = true;
                    return;
                }

                bool res = await Helper.CheckQuote(Name);

                if(!res)
                {
                    Message = "The currency pair you entered could not be found on gate.io";
                    MessageIsVisible = true;
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
