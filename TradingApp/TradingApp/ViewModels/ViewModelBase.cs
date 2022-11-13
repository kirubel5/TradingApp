using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TradingApp.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        #region Fields
        private string imageName;
        private string message;
        private bool shimmerIsActive;
        private bool isRefreshing;
        #endregion

        public ViewModelBase()
        {
            GoToTrackPage = new AsyncCommand(OnGoToTrackPage);
            GoToTradePage = new AsyncCommand(OnGoToTradePage);
            GoToOpenOrderPage = new AsyncCommand(OnGoToOpenOrderPage);
            GoToArbitrageTrackPage = new AsyncCommand(OnGoToArbitragePage);
        }

        #region Commands
        public ICommand GoToTrackPage { get; }
        public ICommand GoToTradePage { get; }
        public ICommand GoToOpenOrderPage { get; }
        public ICommand GoToArbitrageTrackPage { get; }
        #endregion

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

        #region Methods
        private async Task OnGoToTrackPage() => await Shell.Current.GoToAsync("//TrackPage");
        private async Task OnGoToTradePage() => await Shell.Current.GoToAsync("//TradePage");
        private async Task OnGoToOpenOrderPage() => await Shell.Current.GoToAsync("//OpenOrderPage");
        private async Task OnGoToArbitragePage() => await Shell.Current.GoToAsync("//ArbitrageTrackPage");
        #endregion
    }
}
