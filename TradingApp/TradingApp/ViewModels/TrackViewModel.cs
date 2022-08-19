﻿using MvvmHelpers;
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
        private string imageName;
        private string message;
        private bool shimmerIsActive;
        private bool isRefreshing;

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
            TradeCommand = new AsyncCommand(OnTradeButtonClicked);
            RefreshCommand = new AsyncCommand(OnRefresh);
            RightSwipeCommand = new AsyncCommand<object>(OnRightSwipe);
        }

        #region Properties
        public string ImageName
        {
            get => imageName;
            set => SetProperty(ref imageName, value);
        }
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }
        public bool ShimmerIsActive
        {
            get => shimmerIsActive;
            set => SetProperty(ref shimmerIsActive, value);
        }
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }
        #endregion

        #region Commands

        public ICommand AddCommand { get; }
        public ICommand TradeCommand { get; }
        public ICommand RightSwipeCommand { get; }
        public ICommand RefreshCommand { get; }
        #endregion

        #region Methods
        public async Task Load()
        {
            ShimmerIsActive = true;
            TrackedTrades?.Clear();

            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.None)
                {
                    ShimmerIsActive = false;
                    Message = "No internet, please check your connection";
                    ImageName = "NoInternet.png";
                    return;
                }

                var (res, data) = await _dataService.GetSavedTracksAsync();

                if (!res)
                {
                    ShimmerIsActive = false;
                    Message = "Error has occured, please try reloading the page.";
                    ImageName = "SomethingWentWrong.png";
                    return;
                }

                if (data is null || data.Count == 0)
                {
                    ShimmerIsActive = false;
                    Message = "No Saved Trackes.";
                    ImageName = "NoItem.png";
                    return;
                }

                ShimmerIsActive = false;
                trackedTrades = await Helper.LoadTrackInformation(data);

                TrackedTrades?.AddRange(trackedTrades);
            }
            catch (Exception)
            {
                ShimmerIsActive = false;
                Message = "Error has occured, please try reloading the page.";
                ImageName = "SomethingWentWrong.png";
                return;
            }
        }

        public async Task OnRefresh()
        {
            IsRefreshing = true;
            await this.Load();
            IsRefreshing = false;
        }

        private async Task OnAddButtonClicked()
        {
            await Shell.Current.GoToAsync("AddNewTrackPage");
        }

        private async Task OnTradeButtonClicked()
        {
            await Shell.Current.GoToAsync("//TradePage");
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
                await _dataService.DeleteAsync(val);
            }
            catch (Exception)
            {
                DependencyService.Get<IToast>()?.MakeToast("Something went wrong. Please try agian.");
                return;
            }

            DependencyService.Get<IToast>()?.MakeToast("Track Stopped Successfully.");

            await this.Load();
        }
        #endregion
    }
}
