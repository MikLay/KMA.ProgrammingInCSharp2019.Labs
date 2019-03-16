using System.Windows;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Managers;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel, ILoaderOwner
    {
        #region Fields
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;
        #endregion

        #region Properties
        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get => _isControlEnabled;
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        internal MainWindowViewModel()
        {
            LoaderManeger.Instance.Initialize(this);
        }
    }
}
