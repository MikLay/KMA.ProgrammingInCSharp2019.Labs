using System.Windows.Controls;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Navigation;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.ViewModels;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.Views
{
    /// <summary>
    /// Interaction logic for DBListView.xaml
    /// </summary>
    public partial class DBListView : UserControl,INavigatable
    {
        public DBListView()
        {
            InitializeComponent();
            DataContext = new DBListViewModel(delegate { Dispatcher.Invoke(PersonsDataGrid.Items.Refresh); });
        }
    }
}
