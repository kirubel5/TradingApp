using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingApp.Models;
using TradingApp.Services;
using WaissLibraryStandard;
using Xamarin.Essentials;

namespace TradingApp.ViewModels
{
    public class ArbitrageTrackViewModel : ViewModelBase
    {
        #region Fields
        private string baseName;
        private string quoteName;


        private IEnumerable<ArbitragePriceModel> arbitragePrices;
        public ObservableRangeCollection<ArbitragePriceModel> ArbitragePrices { get; set; }

        IValidator _validator;
        #endregion

        public ArbitrageTrackViewModel()
        {
            _validator = new Validator();

            arbitragePrices = new ObservableRangeCollection<ArbitragePriceModel>();
            ArbitragePrices = new ObservableRangeCollection<ArbitragePriceModel>();

            SearchCommand = new AsyncCommand(OnSearch);
        }

        #region Properties
        public string BaseName
        {
            get => baseName;
            set => SetProperty(ref baseName, value);
        }
        public string QuoteName
        {
            get => quoteName;
            set => SetProperty(ref quoteName, value);
        }
        #endregion

        #region Commands
        public ICommand SearchCommand { get; } 
        #endregion

        #region Methods
        private async Task OnSearch()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsRefreshing = true;

            try
            {
                if(_validator.IsEmpty(QuoteName, BaseName))                
                    return;                

                if (Connectivity.NetworkAccess == NetworkAccess.None)
                {
                    Message = "No internet, please check your connection";
                    ImageName = "NoInternet.png";
                    return;
                }

                ArbitragePrices.Clear();

                arbitragePrices = await Helper.LoadArbitrageInformation(QuoteName.ToUpper(), BaseName.ToUpper());
                ArbitragePrices?.AddRange(arbitragePrices);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return;
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        } 
        #endregion
    }
}
