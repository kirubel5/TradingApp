using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingApp.Models;
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

        private readonly IDataService _dataService;
        #endregion

        public AddNewTrackViewModel()
        {
            _dataService = new DataService();

            BackCommand = new AsyncCommand(OnBackButtonClicked);
            SaveCommand = new AsyncCommand(OnSaveButtonClicked);
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
        public ICommand SaveCommand { get; }
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

                Message = "Checking quote, please wait. Don't click the save button again.";
                MessageIsVisible = true;
                bool res = await Helper.CheckQuote(Name);

                if (!res)
                {
                    Message = "The currency pair you entered could not be found on gate.io";
                    MessageIsVisible = true;
                    return;
                }

                Message = "Quote found, saving to Database";

                TrackModel model = new TrackModel 
                { 
                    Name = Name 
                };

                IsBusy = true;
                 
                if(await _dataService.ExistsAsync(model))
                {
                    Message = "Quote already exists in database";
                    IsBusy = false;
                    return;
                }

                if (await _dataService.CreateAsync(model))
                {
                    DependencyService.Get<IToast>()?.MakeToast("Saved Successfully");                    
                    Name = "";
                }
                else
                {
                    DependencyService.Get<IToast>()?.MakeToast("Did not save to database. Please try again");
                }

                IsBusy = false;
                MessageIsVisible = false;
            }
            catch (Exception)
            {
                Message = "Something went wrong, please try again";
                MessageIsVisible = true;
            }
        }
        #endregion
    }
}
