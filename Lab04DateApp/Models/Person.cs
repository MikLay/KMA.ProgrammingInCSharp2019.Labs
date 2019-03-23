using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using JetBrains.Annotations;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Exceptions;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.Models
{
    [Serializable]
    public class Person
    {
        #region Pathes

        private const string DataFilepath = "SerializedDB";
        private const string PersonFileTemplate = "person{0}.bin";

        #endregion

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
            if (birthDay.Year < 1917)
                throw new TooLongAgeException("Wrong Date of born! Too late!");

            Name = name;
            Surname = surname;
            Email = email;
            BirthDay = birthDay;
            IsAdult = CalculateAge() >= 18;
            SunSign = CalculateWest(BirthDay);
            ChineseSign = CalculateAsian(BirthDay.Year);
            IsBirtday = BirthDay.Day == DateTime.Today.Day && BirthDay.Month == DateTime.Today.Month;
        }

        public Person(string name, string surname, string email) : this(name, surname, email, DateTime.Now)
        {
        }

        public Person(string name, string surname, DateTime birthDay) : this(name, surname, "default@gmail.com",
            birthDay)
        {
        }

        #endregion

        #region Properties

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDay { get; private set; }

        public bool IsAdult { get; private set; }

        public string SunSign { get; private set; }
        public string ChineseSign { get; private set; }

        public bool IsBirtday { get; private set; }

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

        #region SpecialMethods

        internal void SetPerson(Person person)
        {
            Name = person.Name;
            Surname = person.Surname;
            Email = person.Email;
            BirthDay = person.BirthDay;
            ChineseSign = person.ChineseSign;
            IsAdult = person.IsAdult;
            IsBirtday = person.IsBirtday;
            SunSign = person.SunSign;
        }

        #endregion

        #region Serialization

        private void SaveTo([NotNull] string filename)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Directory.CreateDirectory(Path.GetDirectoryName(filename) ?? throw new InvalidOperationException());
                Stream stream = new FileStream(path: filename,
                    mode: FileMode.Create,
                    access: FileAccess.Write,
                    share: FileShare.None);
                formatter.Serialize(serializationStream: stream, graph: this);
                stream.Close();
            }
            catch (IOException e)
            {
                MessageBox.Show("Error with saving: " + e.Message);
            }
        }

        private static Person LoadFrom(string filename)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read);
                var person = (Person) formatter.Deserialize(stream);
                stream.Close();
                return person;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async void LoadAll(List<Person> persons, Action action)
        {
            await Task.Run(() =>
            {
                if (!Directory.Exists(DataFilepath))
                {
                    Directory.CreateDirectory(DataFilepath);
                    persons.AddRange(CreatePersons.SpawnPersons(50));
                    Save(persons);
                }
                else
                {
                    persons.AddRange(Directory.EnumerateFiles(DataFilepath).Select(LoadFrom));
                }

                action();
            });
        }

        public static void Save(List<Person> persons)
        {
            if (persons == null)
            {
                MessageBox.Show("Can`t save NULL");
            }

            var i = 0;
            persons?.ForEach(delegate(Person p)
            {
                p.SaveTo(Path.Combine(DataFilepath, string.Format(PersonFileTemplate, i++)));
            });
            string extraFile;
            while (File.Exists(extraFile = Path.Combine(DataFilepath, string.Format(PersonFileTemplate, i++))))
                File.Delete(extraFile);
        }

        private static class CreatePersons
        {
            internal static List<Person> SpawnPersons(int count)
            {
                var persons = new List<Person>();
                var random = new Random();
                for (var i = 0; i < count; ++i)
                {
                    var name = Names[random.Next(Names.Length)];
                    var surname = Surnames[random.Next(Surnames.Length)];

                    persons.Add(new Person(name, surname,
                        $"{name}.{surname}@{EmailsEndings[random.Next(EmailsEndings.Length)]}",
                        DateTime.Now.AddYears(-random.Next(10, 80)).AddDays(-random.Next(31))
                            .AddMonths(-random.Next(12))));
                }

                return persons;
            }

            private static readonly string[] Names =
            {
                "Brigid","Vivian","Trinh","Vincente","Russell","Jaqueline","Tyisha","Sherrill","Teddy",
                "Gregoria","Emilee", "Vivienne", "Luba", "Sharie", "Jo", "Paulette", "Merrill", "Jolyn", "Gerda",
                "Osvaldo", "Rafaela", "Rheba", "Ava", "Meghan", "Marsha", "Betty", "Rozanne", "Margit", "Rupert"
            };

            private static readonly string[] Surnames =
            {
                "Ortega","Smith","Johnson","Williamson","Jones","Brown","Davis","Miller","Wilson","Moore","Taylor",
                "Anderson","Wise","Phelps","Daniel","Camacho","Burnett","Daugherty","Valencia","Burns","Werner","Odom",
                "Lucero","Key","Frazier","Conley","Huffman","Carlson","Anderson","Goodwin","Lewis","Lee","Walker","Hall",
                "Allen","Young","Hernandez","Wright","Lopez","Hill","Scott","Green","Adams","Baker","Gonzalez","Nelson",
                "Carter","Mitchell","Perez","Roberts","Turner","Phillips","Campbell","Parker","Evans","Edwards","Collins"
            };

            private static readonly string[] EmailsEndings =
            {
                "gmail.com",
                "yahoo.com",
                "msn.com",
                "hotmail.com",
                "aol.com"
            };
        }

        #endregion
    }
}