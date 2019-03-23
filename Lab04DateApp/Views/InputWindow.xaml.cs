using System;
using System.Windows;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Models;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Navigation;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.ViewModels.InputDate;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.Views
{
    /// <summary>
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        internal InputWindow(Action<Person> processAction, Person person = null)
        {
            InitializeComponent();
            DataContext = new InputDateWindowViewModel(person, delegate(Person processedPerson)
            {
                Close();
                processAction(processedPerson);
            });
        }
    }
}