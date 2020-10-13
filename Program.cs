using System;
using System.Collections.Generic;
using System.Text;

namespace oop_4
{
    internal class Owner
    {
        internal Owner() : this("Default_Id", "Default_Name", "Default_Org") {}
        internal Owner(string id, string name, string org)
        {
            Id = id;
            Name = name;
            Org = org;
        }

        internal string Id { get; set; }
        internal string Name { get; set; }
        internal string Org { get; set; }
    }

    internal class Date
    {
        int _mounth;
        int _day;
        internal int Year { get; set; }
        internal int Mounth {
            get
            {
                return _mounth;
            }
            set
            {
                _mounth = value > 0 && value < 13 ? value : DateTime.Now.Month; 
            } 
        }
        internal int Day
        {
            get
            {
                return _day;
            }
            set
            {
                _day = value > 0 && value < 32 ? value : DateTime.Now.Day;
            }
        }

        internal Date() : this(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) { }
        internal Date(int year, int mounth, int day) {
            Year = year;
            Mounth = mounth;
            Day = day;
        }
    }

    internal class Set
    {
        List<int?> _data;
        Date _date;
        Owner _owner;

        internal Set()
        {
            _data = new List<int?> { };
            _date = new Date();
            _owner = new Owner();
        }

        internal Set(Date date, Owner owner)
        {
            _data = new List<int?> { };
            _owner = owner;
            _date = date;
        }

        internal int? this[int i]
        {
            get
            {
                if (i > _data.Count)
                    return null;

                return _data[i];
            }
        }
        internal int Length
        {
            get { return _data.Count; }
        }


        public static bool operator | (Set set, int? element) // проверка на принадлежность элемента
        {
            return set._data.Contains(element);
        }
   
        public  static bool operator << (Set set, int? element) // добавление в множество
        {
            if (set | element)
                return false;

            set._data.Add(element);
            return true;
        }

        public static bool operator >> (Set set, int? element) // удаление из множества
        {
            return set._data.Remove(element);
        }

        public static bool operator > (Set set1, Set set2)
        {
            for (int i = 0; i < set2._data.Count; i++)
            {
                if (!(set1 | set2[i]))
                    return false;     
            }
            return true;
        }

        public static bool operator < (Set set1, Set set2)
        {
            return false;
        }

        public static bool operator != (Set set1, Set set2)
        {
            return !set1._data.Equals(set2._data);
        }

        public static bool operator == (Set set1, Set set2)
        {
            return set1._data.Equals(set2._data);
        }

        public static Set operator % (Set set1, Set set2)
        {
            Set buff = new Set();
            for (int i = 0; i < set1._data.Count; i++)
            {
                bool kostyl; 
                if (set2 | set1[i])
                    kostyl = buff << set1[i];
            }
            return buff;
        }
    }

    internal static class StatisticOperation
    {
        internal static bool isOrdered(this Set set) // упорядочены ли элементы множества
        {
            for (int i = 1; i < set.Length; i++)
            {
                if (!(set[i] > set[i - 1]))
                {
                    return false;
                }
            }
            return true;
        }

        internal static int? CountSum(this Set set)
        {
            int? sum = 0;

            for (int i = 0; i < set.Length; i++)
            {
                sum += set[i] ?? 0;
            }

            return sum;
        }


        internal static string FindShortest(this string str, char symb = ' ')
        {
            string[] words = str.Split(symb);

            int shortest = 0;
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length < words[shortest].Length)
                {
                    shortest = i;
                }
            }
            return words[shortest];
        }
    } 

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Set a = new Set();

            bool n = a << 1;
            n = a << 2;
            n = a << 3;


            int? z = 2, y = 1;
      

            Console.WriteLine(a.CountSum());
        }
    }
}
