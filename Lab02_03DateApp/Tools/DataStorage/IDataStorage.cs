using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Models;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.DataStorage
{
    internal interface IDataStorage
    {
        bool UserExists(string login);

        Person GetUserByLogin(string login);

        void AddUser(Person user);
    }
}
