using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Managers;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Navigation;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.ViewModels.PrecessedDate
{
    internal class InfoDateViewModel:BaseViewModel
    {
        #region Fields
 
        #region Commands
        private RelayCommand<object> _backCommand;
        private RelayCommand<object> _refreshCommand;

        #endregion
        #endregion

        #region Properties
        public string Age => StationManager.CurrentUser.CalculateAge().ToString();

        public string Name => StationManager.CurrentUser.Name;
        public string Surname => StationManager.CurrentUser.Surename;
        public string Email => StationManager.CurrentUser.Email;
        public string Adult => StationManager.CurrentUser.IsAdult()?"Yes":"No";
        public string SunSign => StationManager.CurrentUser.SunSign();
        public string ChineseSign => StationManager.CurrentUser.ChineseSign();
        public string IsBirthDay => StationManager.CurrentUser.IsBirtday() ? "Yes" : "No";
        #endregion

        #region Commands


        public RelayCommand<object> RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new RelayCommand<object>(o =>
                {
                    OnPropertyChanged(nameof(Age));
                    OnPropertyChanged(nameof(Name));
                    OnPropertyChanged(nameof(Surname));
                    OnPropertyChanged(nameof(Email));
                    OnPropertyChanged(nameof(SunSign));
                    OnPropertyChanged(nameof(ChineseSign));
                    OnPropertyChanged(nameof(IsBirthDay));
                    OnPropertyChanged(nameof(Adult));
                }, o => true));
            }
        }

        public RelayCommand<object> BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand<object>(o =>
                {
                    NavigationManager.Instance.Navigate(ViewType.Input);
                    
                }, o => true));
            }
        }


        #endregion

    }
}
