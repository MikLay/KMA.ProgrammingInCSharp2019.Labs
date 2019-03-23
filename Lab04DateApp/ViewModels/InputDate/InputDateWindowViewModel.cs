using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Exceptions;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Models;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Managers;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.ViewModels.InputDate
{
    internal class InputDateWindowViewModel : BaseViewModel
    {
        public InputDateWindowViewModel(Person person, Action<Person> onProcessAction)
        {
            if (person != null)
            {
                Email = person.Email;
                Name = person.Name;
                Surname = person.Surname;
                BirthDate = person.BirthDay;
            }
            _onProcessAction = onProcessAction;
        }

        #region Fields

        private string _email;
        private string _name;
        private string _surname;
        private DateTime _birthDate = DateTime.Today;

        #region Commands
        private ICommand _processPersonCommand;
        #endregion

        #region Actions
        private readonly Action<Person> _onProcessAction;
        #endregion
      
        #endregion

        #region Properties

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand ProcessCommand =>
            _processPersonCommand ?? (_processPersonCommand =
                new RelayCommand<object>(ProcessPerson, CanSignUpExecute));


        private async void ProcessPerson(object obj)
        {
            Person person = null;
            LoaderManeger.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    person = new Person(_name, _surname, _email, _birthDate);
                   // StationManager.CurrentUser = person;
                }
                catch (EmailException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                catch (FutureAgeException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                catch (TooLongAgeException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                return true;
            });
            LoaderManeger.Instance.HideLoader();
            if (result)
            {
                _onProcessAction(person);
            }
        }

        #endregion

        #region Private Methods

        private bool CanSignUpExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_name) && 
                   !String.IsNullOrWhiteSpace(_surname)&&
                   !String.IsNullOrWhiteSpace(_email)&&
                   !String.IsNullOrWhiteSpace(_birthDate.ToString(CultureInfo.InvariantCulture))&&
                CheckEmail();
        }



        private bool CheckEmail()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            bool checkE = false;
            if (!string.IsNullOrWhiteSpace(_email))
            {
                checkE = regex.Match(_email).Success;
            }
            return checkE;
        }
        #endregion
    }
}