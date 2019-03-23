using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Models;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools
{
    public static class SortExtension
    {
        public static readonly string[] SortFiltertOptions =
            Array.ConvertAll(typeof(Person).GetProperties(), (property) => property.Name);

        public static List<Person> SortByProperty(this List<Person> persons, string property, bool ascending)
        {
            return Array.IndexOf(SortFiltertOptions, property) >= 0
                ?
                (ascending
                    ?
                    (from p in persons orderby p.GetType().GetProperty(property)?.GetValue(p, null) ascending select p).ToList()
                    :
                    (from p in persons orderby p.GetType().GetProperty(property)?.GetValue(p, null) descending select p).ToList()
                )
                : persons;
        }

        public static List<Person> FilterByProperty(this List<Person> persons, string property, string query)
        {
            if (Array.IndexOf(SortFiltertOptions, property) < 0) return new List<Person>();

            query = query.ToLower();
            return (from p in persons
                where (p.GetType().GetProperty(property)?.GetValue(p, null)).ToString().ToLower().Contains(query)
                select p).ToList();
        }
    }
}
