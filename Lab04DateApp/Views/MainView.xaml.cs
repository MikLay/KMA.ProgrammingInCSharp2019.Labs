using System.Windows.Controls;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Navigation;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl,INavigatable
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}
