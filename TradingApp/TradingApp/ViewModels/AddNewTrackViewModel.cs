using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TradingApp.ViewModels
{
    public class AddNewTrackViewModel : ViewModelBase
    {
        #region Fields
        private string message;
        #endregion

        public AddNewTrackViewModel()
        {
            BackCommand = new AsyncCommand(OnBackButtonClicked);
        }

        #region Properties
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }
        #endregion

        #region Commands
        public ICommand BackCommand { get; }
        #endregion

        #region Methods

        private async Task OnBackButtonClicked()
        {
            await Shell.Current.GoToAsync("..");
        }
        #endregion
    }
}
