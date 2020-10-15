using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oop_4
{
    

    internal class Set
    {
        internal class Owner
        {
            internal Owner() : this("default_Id", "default_Name", "default_Org") { }
            internal Owner(string id, string name, string org)
            {
                Id = id;
                Name = name;
                Org = org;
            }
            internal string Id { get; set; }
            internal string Name { get; set; }
            internal string Org { get; set; }
            public override string ToString()
            {
                return $"{Id} / {Name} / {Org}";
            }
        }
        internal class MyDate
        {
            int _mounth;
            int _day;
            internal int Year { get; set; }
            internal int Mounth
            {
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
            internal MyDate() : this(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) { }
            internal MyDate(int year, int mounth, int day)
            {
                Year = year;
                Mounth = mounth;
                Day = day;
            }
            public override string ToString()
            {
                StringBuilder buff = new StringBuilder();
                buff.Append($"{Day}.{Mounth}.{Year}");
                return buff.ToString();
            }
        }

        List<int> _data;
        internal MyDate Day { get; set; }
        internal Owner SetOwner { get; set; }

        internal int Length
        {
            get { return _data.Count; }
        }

        internal Set()
        {
            _data = new List<int> { };
            Day = new MyDate();
            SetOwner = new Owner();
        }

        internal Set(MyDate date, Owner set_owner)
        {
            _data = new List<int> { };
            SetOwner = set_owner;
            Day = date;
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

        public static bool operator | (Set set, int element) // проверка на принадлежность элемента
        {
            return set._data.Contains(element);
        }
   
        public  static bool operator << (Set set, int element) // добавление в множество
        {
            if (set | element)
                return false;

            set._data.Add(element);
            return true;
        }

        public static bool operator >> (Set set, int element) // удаление из множества
        {
            return set._data.Remove(element);
        }

        public static bool operator > (Set set1, Set set2) // Является второе подмножеством первого
        {
            for (int i = 0; i < set2._data.Count; i++)
            {
                if (!(set1 | (int)set2[i]))
                    return false;     
            }
            return true;
        }

        public static bool operator < (Set set1, Set set2) // является ли первое подмножеством второго
        {
            return set2 > set1;
        }

        public static Set operator %(Set set1, Set set2) // Пересечение
        {
            Set buff = new Set();
            for (int i = 0; i < set1._data.Count; i++)
            {
                bool kostyl;
                if (set2 | (int)set1[i])
                    if (!(buff << (int)set1[i])) // засунула вставку элемента в if, потому что иначе оно не работает, а так пусть хоть проверка будет
                        break;
            }
            return buff;
        }

        public static bool operator != (Set set1, Set set2)
        {
            return !set1.Equals(set2);
        }

        public static bool operator == (Set set1, Set set2)
        {
            return set1.Equals(set2);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            for(int i = 0; i < Length; i++)
            {
                hash += (int)(Math.Pow((double)this[i], i));
            }
            return hash;
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder buff = new StringBuilder();
            for (int i = 0; i < Length; i++)
            {
                buff.Append(this[i] + " ");
            }
            buff.Append($"| дата создания: {Day.ToString() } | владелец: {SetOwner.ToString()} ");
            return buff.ToString();
        }
    }
    
    internal static class StatisticOperation
    {
        internal static Set OrderBy(this Set set) // упорядочивание элементов
        {
            Set ordered = new Set(set.Day, set.SetOwner);


            for (int i = 0; i < set.Length; i++)
            {
                if (!(ordered << (int)set[i])) { Console.WriteLine("Вставка не удалась"); }
            }

            for (int i = 0; i < set.Length; i++)
            {
                // найти минимальное
                // засунуть минимальное в буферное множество
                // найти минимальное среди всех, не считая последнюю позицию                
                int min = 0;
                for (int j = 0; j < ordered.Length - i; j++)
                {
                    min = ordered[j] < ordered[min] ? j : min;
                }
                int buff = (int)ordered[min];
                if (!(ordered >> buff)) { Console.WriteLine("Удаление не удалось"); }
                if (!(ordered << buff)) { Console.WriteLine("Вставка не удалась"); }
            }
            return ordered;
        }

        internal static int CountSum(this Set set) // сумма элементов
        {
            int sum = 0;
            for (int i = 0; i < set.Length; i++)
            {
                sum += (int)set[i];
            }
            return sum;
        }

        internal static int MaxMin(this Set set) // разница между максимальным и минимальным
        {
            int max = 0, min = 0;
            for(int i = 0; i < set.Length; i++)
            {
                max = set[i] > set[max] ? i : max;
                min = set[i] < set[min] ? i : min;
            }
            return (int)(set[max] - set[min]);
        }

        internal static int getLen(this Set set) // мне действительно нужно это сделать? Ну ладно, я девочка хорошая, раз сказано в задании - сделаю
        {
            return set.Length;
        }

        //////////////////////////////////////////////
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
            Set.Owner anton = new Set.Owner("19102001", "Евгений", "Shalom Contracts");
            Set.MyDate day = new Set.MyDate(2019, 09, 15);
            Set first = new Set(day, anton);
            if (first << 2) { Console.WriteLine("Вставка удалась"); } else { Console.WriteLine("Не вставлено"); }
            if (first << 2) { Console.WriteLine("Вставка удалась"); } else { Console.WriteLine("Не вставлено"); }
            if (first << 3) { Console.WriteLine("Вставка удалась"); } else { Console.WriteLine("Не вставлено"); }
            if (first << 4) { Console.WriteLine("Вставка удалась"); } else { Console.WriteLine("Не вставлено"); }
            if (first << 5) { Console.WriteLine("Вставка удалась"); } else { Console.WriteLine("Не вставлено"); }
            if (first << 6) { Console.WriteLine("Вставка удалась"); } else { Console.WriteLine("Не вставлено"); }
            Console.WriteLine("Первое множество: " + first.ToString());
            Console.WriteLine("Упорядоченный вариант: " + (first.OrderBy()).ToString());
            Console.WriteLine("Cумма его элементов: " + first.CountSum());
            Console.WriteLine("Максимальный элемент минус минимальный: " + first.MaxMin());
            Console.WriteLine("Количество его элементов: " + first.getLen());
            if (first >> 6) { Console.WriteLine("Удален элемент 6"); } else { Console.WriteLine("Элемент 6 не удален"); }
            if (first >> 56) { Console.WriteLine("Удален элемент 56"); } else { Console.WriteLine("Элемент 56 не удален"); }

            Set second = new Set();
            if (second << 10) { Console.WriteLine("Вставка удалась"); } else { Console.WriteLine("Не вставлено"); }
            if (second << 2) { Console.WriteLine("Вставка удалась"); } else { Console.WriteLine("Не вставлено"); }
            if (second << 56) { Console.WriteLine("Вставка удалась"); } else { Console.WriteLine("Не вставлено"); }
            if (second << 5) { Console.WriteLine("Вставка удалась"); } else { Console.WriteLine("Не вставлено"); }
            if (second << 6) { Console.WriteLine("Вставка удалась"); } else { Console.WriteLine("Не вставлено"); }
            Console.WriteLine("Второе множество: " + second.ToString());
            Console.WriteLine("Упорядоченный вариант: " + (second.OrderBy()).ToString());
            Console.WriteLine("Содержится ли второе множество в первом? " + (second < first));

            Set third = first % second;
            Console.WriteLine("Третье множество -- пересечение первых двух: " + third.ToString());
            Console.WriteLine("Содержится ли пересечение двух множеств в первом? " + (third < first));
            Console.WriteLine("Равняется ли пересечение двух множеств третьему множеству? " + (third == first%second));
            Console.WriteLine("Верно ли, что третье множество не равняется первому? " + (third != first));           
        }
    }
}
