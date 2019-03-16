using System;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.Exceptions
{
    internal class FutureAgeException: Exception
    {
        public FutureAgeException(string messege) : base(messege)
        {
        }
    }
}
