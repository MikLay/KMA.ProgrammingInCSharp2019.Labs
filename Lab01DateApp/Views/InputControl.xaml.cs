using System.Windows.Controls;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.ViewModels;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp
{
    /// <summary>
    /// Interaction logic for InputControl.xaml
    /// </summary>
    public partial class InputControl:UserControl
    {
        public InputControl()
        {
            InitializeComponent();
            DataContext = new InputDateViewModel();
        }
    }
}
