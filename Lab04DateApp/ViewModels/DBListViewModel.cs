using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Data;
using JetBrains.Annotations;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Models;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Managers;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Views;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.ViewModels
{
    // ReSharper disable once InconsistentNaming
    class DBListViewModel : INotifyPropertyChanged
    {
        #region Fields

        private List<Person> _persons;
        private Person _selectedPerson;
        private readonly Action _refreshAction;
        private string _filterByWord;
        private bool _isSortedAsc = true;


        #region Static fields

        private static CollectionView _sortFilterOptionsCollection;

        public static CollectionView SortOption => _sortFilterOptionsCollection ??
                                                   (_sortFilterOptionsCollection =
                                                       new CollectionView(SortExtension.SortFiltertOptions));

        #endregion


        #region Commands

        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _editCommand;
        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _sortCommand;
        private RelayCommand<object> _clearFilterCommand;

        #endregion

        #endregion

        #region Constructors

        public DBListViewModel(Action updateAllGrid)
        {
            _refreshAction = updateAllGrid;
            _persons = new List<Person>();
            Person.LoadAll(PersonsList, UpdatePersonsGrid);
        }

        #endregion

        #region Properties

        public string SelectedFilterProperty { get; set; }

        public string SelectedPersonInitials { get; private set; }

        public string FilterByWord
        {
            get => _filterByWord;
            set
            {
                _filterByWord = value;
                SelectedPerson = null;
                UpdatePersonsGrid();
            }
        }

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                if (_selectedPerson == null) return;
                SelectedPersonInitials = $"{_selectedPerson.Name} {_selectedPerson.Surname}";
                OnPropertyChanged(nameof(SelectedPersonInitials));
            }
        }

        public List<Person> PersonsList =>
            (string.IsNullOrEmpty(SelectedFilterProperty) || string.IsNullOrEmpty(FilterByWord))
                ? _persons
                : _persons.FilterByProperty(SelectedFilterProperty, FilterByWord);

        #endregion

        #region Commands

        public RelayCommand<object> AddCommand =>
            _addCommand ?? (_addCommand = new RelayCommand<object>(AddPerson, o => true));


        public RelayCommand<object> DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new RelayCommand<object>(DeletePerson, o => _selectedPerson != null));

        public RelayCommand<object> EditCommand =>
            _editCommand ?? (_editCommand = new RelayCommand<object>(EditPerson, o => _selectedPerson != null));

        public RelayCommand<object> SortCommand =>
            _sortCommand ?? (_sortCommand =
                new RelayCommand<object>(SortPersonsList, o => !string.IsNullOrEmpty(SelectedFilterProperty)));

        public RelayCommand<object> ClearFilterCommand =>
            _clearFilterCommand ?? (_clearFilterCommand = new RelayCommand<object>((o) =>
                {
                    FilterByWord = "";
                    OnPropertyChanged($"FilterByWord");
                },
                o => !string.IsNullOrEmpty(FilterByWord)));

        #endregion

        #region Private methods

        private void UpdatePersonsGrid()
        {
            Person.Save(_persons);
            OnPropertyChanged(nameof(PersonsList));
            _refreshAction();
        }


        private void AddPerson(object o)
        {
            var registerWindow = new InputWindow(delegate(Person newPerson)
            {
                PersonsList.Add(newPerson);
                UpdatePersonsGrid();
            });
            registerWindow.Show();
        }

        private async void DeletePerson(object o)
        {
            LoaderManeger.Instance.ShowLoader();
            await Task.Run((() =>
            {
                _persons.Remove(SelectedPerson);
                UpdatePersonsGrid();
            }));
            LoaderManeger.Instance.HideLoader();
        }

        private void EditPerson(object o)
        {

            LoaderManeger.Instance.ShowLoader();
            var personToEdit = _selectedPerson;
            var editWindow = new InputWindow(delegate(Person edited)
            {
                personToEdit.SetPerson(edited);
                UpdatePersonsGrid();
            }, _selectedPerson);
            editWindow.Show();
            LoaderManeger.Instance.HideLoader();
        }

        private async void SortPersonsList(object o)
        {
            LoaderManeger.Instance.ShowLoader();
            await Task.Run(() =>
            {
                _persons = _persons.SortByProperty(SelectedFilterProperty, _isSortedAsc);
                // Change order
                _isSortedAsc = !_isSortedAsc;
                UpdatePersonsGrid();
            });
            LoaderManeger.Instance.HideLoader();
        }

        #endregion

        #region PropertyChangedImpl

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}