using MvvmHelpers;
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
    public class TrackViewModel : ViewModelBase
    {
        #region Fields

        private IEnumerable<TrackModel> trackedTrades;
        public ObservableRangeCollection<TrackModel> TrackedTrades { get; set; }
        private readonly IDataService _dataService;
        #endregion

        public TrackViewModel()
        {
            _dataService = new DataService();

            trackedTrades = new ObservableRangeCollection<TrackModel>();
            TrackedTrades = new ObservableRangeCollection<TrackModel>();

            AddCommand = new AsyncCommand(OnAddButtonClicked);
            LoadCommand = new AsyncCommand(Load);
            RightSwipeCommand = new AsyncCommand<object>(OnRightSwipe);
        }

        #region Properties
       
        #endregion

        #region Commands

        public ICommand AddCommand { get; }
        public ICommand RightSwipeCommand { get; }
        public ICommand LoadCommand { get; }
        #endregion

        #region Methods
        public async Task Load()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsRefreshing = true;
            ShimmerIsActive = true;
            TrackedTrades?.Clear();

            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.None)
                {
                    Message = "No internet, please check your connection";
                    ImageName = "NoInternet.png";
                    return;
                }

                var (res, data) = await _dataService.GetSavedTracksAsync();

                if (!res)
                {
                    Message = "Error has occured, please try reloading the page.";
                    ImageName = "SomethingWentWrong.png";
                    return;
                }

                if (data is null || data.Count == 0)
                {
                    Message = "No Saved Tracks.";
                    ImageName = "NoItem.png";
                    return;
                }

                trackedTrades = await Helper.LoadTrackInformation(data);
                TrackedTrades?.AddRange(trackedTrades);
            }
            catch (Exception)
            {               
                Message = "Error has occured, please try reloading the page.";
                ImageName = "SomethingWentWrong.png";
                return;
            }
            finally
            {
                ShimmerIsActive = false;
                IsRefreshing = false;
                IsBusy = false;
            }
        }

        private async Task OnAddButtonClicked()
        {
            await Shell.Current.GoToAsync("AddNewTrackPage");
        }       

        private async Task OnRightSwipe(object obj)
        {
            try
            {
                var duration = TimeSpan.FromMilliseconds(100);
                Vibration.Vibrate(duration);
            }
            catch (Exception)
            {
                // Dont do any thing, just move on
            }

            var val = obj as TrackModel;

            bool res = await Shell.Current.DisplayAlert("Stop Tracking", $"Are you sure you want to stop " +
                $"tracking: {val.Name}?", "Ok", "Cancel");

            if (!res)
                return;

            try
            {
                if (await _dataService.DeleteAsync(val))
                {
                    DependencyService.Get<IToast>()?.MakeToast("Track Stopped Successfully");
                    await this.Load();
                }
                else
                {
                    DependencyService.Get<IToast>()?.MakeToast("Something went wrong. Please try agian.");
                    return;
                }
            }
            catch (Exception)
            {
                DependencyService.Get<IToast>()?.MakeToast("Something went wrong. Please try agian.");
                return;
            }
        }
        #endregion
    }
}
