using System.Windows;
using System.Windows.Controls;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Managers;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Navigation;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.ViewModels;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContentOwner
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            InitializeApp();
        }

        private void InitializeApp()
        {
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.Input);
        }


        public ContentControl ContentControl => _contentControl;
    }
}