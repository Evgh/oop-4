using System;
using System.Collections.Generic;
using System.Text;

namespace oop_4
{
    internal class Set
    {
        List<int?> _data;

        internal Set()
        {
            _data = new List<int?> { };
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

        public static bool operator | (Set set, int? element) // проверка на принадлежность элемента
        {
/*            foreach (int? item in set._data)
            {
                if (!item.Equals(element))
                    return false;
            }*/
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

    public static class MyStringExtension
    {
        public static string FindShortest(this string str, char symb = ' ')
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

            String ss = "zbub_Anton_L";

            Console.WriteLine(ss.FindShortest(' '));
        }
    }
}
