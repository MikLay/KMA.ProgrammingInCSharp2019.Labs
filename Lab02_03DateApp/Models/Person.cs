using System;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Exceptions;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.Models
{
    internal class Person
    {
        #region Fields

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

        #region Constructors
        public Person(string name, string surname, string email, DateTime birthDay)
        {
         if (birthDay > DateTime.Now)
             throw new FutureAgeException("The person is not born! Too early!");
         if(birthDay.Year < 1917)
             throw new TooLongAgeException("Wrong Date of born! Too late!");

            Name = name;
            Surename = surname;
            Email = email;
            BirthDay = birthDay;
        }

        public Person(string name, string surname, string email): this(name, surname, email, DateTime.Now) { }

        public Person(string name, string surname, DateTime birthDay) : this(name,surname,"default@gmail.com", birthDay) { }
        #endregion

        #region Properties

        internal string Name { get; set; }

        internal string Surename { get; set; }

        internal string Email { get; set; }

        internal DateTime BirthDay { get; set; }

        public bool IsAdult()
        {
            return CalculateAge() >= 18;
        }

        public string SunSign()
        {
            return CalculateWest(BirthDay);
        }

        public string ChineseSign()
        {
            return CalculateAsian(BirthDay.Year);
        }

        public bool IsBirtday()
        {
            return BirthDay.Day == DateTime.Today.Day && BirthDay.Month == DateTime.Today.Month;
        }
        #endregion

        #region PrivateMethods

        internal int CalculateAge()
        {
            return ((DateTime.Today - BirthDay).Days / 365);
        }

        private string CalculateWest(DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                    return date.Day <= 20 ? "Capricorn" : " Aquarius";
                case 2:
                    return date.Day <= 19 ? " Aquarius" : " Pisces";
                case 3:
                    return date.Day <= 20 ? "Pisces" : "Aries";
                case 4:
                    return date.Day <= 20 ? "Aries" : "Taurus";
                case 5:
                    return date.Day <= 21 ? "Taurus" : "Gemini";
                case 6:
                    return date.Day <= 22 ? "Gemini" : "Cancer";
                case 7:
                    return date.Day <= 22 ? "Cancer" : "Leo";
                case 8:
                    return date.Day <= 23 ? "Leo" : "Virgo";
                case 9:
                    return date.Day <= 23 ? "Virgo" : "Libra";
                case 10:
                    return date.Day <= 23 ? "Libra" : "Scorpio";
                case 11:
                    return date.Day <= 22 ? "Scorpio" : "Sagittarius";
                case 12:
                    return date.Day <= 21 ? "Sagittarius" : "Capricorn";
                default:
                    return "Mogylianec";
            }
        }

        private string CalculateAsian(int year)
        {
            return Enum.GetName(typeof(AsianZodiacSigns), year % 12);
        }


        #endregion
    }
}
