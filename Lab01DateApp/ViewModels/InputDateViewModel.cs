using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.ViewModels
{

    internal class InputDateViewModel:INotifyPropertyChanged
    {

        
        #region Fields

        private string _age = "";
        private DateTime _birthDate = DateTime.Today;
        private string _zodiacNormal = "";
        private string _zodiacAsian = "";

        #region Enums

        enum AsianZodiacSigns
        {
            Monkey,
            Rooster,
            Dog,
            Pig,
            Rat,
            Ox,
            Tiger,
            Rabbit,
            Dragon,
            Snake,
            Horse,
            Ram
        };


        #endregion

        #endregion

        #region Commands

        private RelayCommand<object> _calculateCommand;


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

        public string Age
        {
            get => this._age.ToString();
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public string ZodiacNormal
        {
            get => _zodiacNormal;
            set
            {
                _zodiacNormal = value;
                OnPropertyChanged();
            }
        }

        public string ZodiacAsian
        {
            get => _zodiacAsian;
            set
            {
                _zodiacAsian = value;
                OnPropertyChanged();
            }
        }

        #region Commands
        
        public RelayCommand<object> CalculateCommand
        {
            get { return _calculateCommand ?? (_calculateCommand = new RelayCommand<object>(o =>
                             {
                                 CalculateAge(BirthDate);
                                 OnPropertyChanged();
                             }, o=>true)); }
        }

        private async void CalculateAge(DateTime date)
        {
            int age = ((DateTime.Today - date).Days / 365) ;
            if (age > 135 || date > DateTime.Today)
            {
                MessageBox.Show($"Error, wrong input!\nCheck date {_birthDate}!");
                return;
            }else if (date.Day == DateTime.Today.Day && date.Month == DateTime.Today.Month)
            {
                MessageBox.Show("Hop - Hey La-la-ley\nHappy birthday lucky rich witch!!!");
            }

            Age = age.ToString();
            await Task.Run(()=> CalculateWest(date));
            await Task.Run(()=>CalculateAsian(date.Year));
            
        }
        #endregion

        private void CalculateWest(DateTime date)
        {
            string sign = "";
            switch (date.Month)
            {
                case 1:
                    sign = date.Day <= 20 ? "Capricorn" : " Aquarius";
                    break;
                case 2:
                    sign = date.Day <= 19 ? " Aquarius" : " Pisces";
                    break;
                case 3:
                    sign = date.Day <= 20 ? "Pisces" : "Aries";
                    break;
                case 4:
                    sign = date.Day <= 20 ? "Aries" : "Taurus";
                    break;
                case 5:
                    sign = date.Day <= 21 ? "Taurus" : "Gemini";
                    break;
                case 6:
                    sign = date.Day <= 22 ? "Gemini" : "Cancer";
                    break;
                case 7:
                    sign = date.Day <= 22 ? "Cancer" : "Leo";
                    break;
                case 8:
                    sign = date.Day <= 23 ? "Leo" : "Virgo";
                    break;
                case 9:
                    sign = date.Day <= 23 ? "Virgo" : "Libra";
                    break;
                case 10:
                    sign = date.Day <= 23 ? "Libra" : "Scorpio";
                    break;
                case 11:
                    sign = date.Day <= 22 ? "Scorpio" : "Sagittarius";
                    break;
                case 12:
                    sign = date.Day <= 21 ? "Sagittarius" : "Capricorn";
                    break;
                default:
                    sign = "Mogylianec";
                    break;
            }

            ZodiacNormal = sign;
        }

        private void CalculateAsian(int year)
        {
            ZodiacAsian = Enum.GetName(typeof(AsianZodiacSigns), year%12);
        }



        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
