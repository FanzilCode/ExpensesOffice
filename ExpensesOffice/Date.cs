using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeExpenses
{
    class Date
    {
        // месяц
        public int month { get; private set; }
        // год
        public int year { get; private set; }
        public string monthStr { get; private set; }
        // конструктор
        public Date(DateTime date)
        {
            month = date.Month;
            year = date.Year;
            monthStr = date.Month.ToString();
        }
        // конструктор для чтения из файла
        public Date(string[] arr)
        {
            month = Convert.ToInt32(arr[0]);
            year = Convert.ToInt32(arr[1]);
        }
        public void Print()
        {
            Console.WriteLine($"Месяц: {monthStr}" +
                $"Год: {year}");
        }
        public override string ToString()
        {
            return $"{month}%{year}";
        }
        public static bool operator ==(Date d1, Date d2)
        {
            return (d1.month == d2.month && d1.year == d2.year);
        }
        public static bool operator !=(Date d1, Date d2)
        {
            return !(d1 == d2);
        }
        public static bool operator >(Date d1, Date d2)
        {
            if (d1.year > d2.year)
                return true;
            else
            {
                if (d1.year < d2.year)
                    return (d1.month > d2.month);
                else
                    return false;
            }
        }
        public static bool operator <(Date d1, Date d2)
        {
            return !(d1 > d2);
        }
    }
}
