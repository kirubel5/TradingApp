using MvvmHelpers;

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
    }
}
