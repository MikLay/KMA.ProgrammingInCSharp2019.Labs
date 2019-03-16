using System.Windows.Controls;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Navigation;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.ViewModels.PrecessedDate;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.Views.Submited
{
    /// <summary>
    /// Interaction logic for InfoView.xaml
    /// </summary>
    public partial class InfoView : UserControl,INavigatable
    {
        internal InfoView()
        {
            InitializeComponent();
            DataContext = new InfoDateViewModel();
        }
    }
}
