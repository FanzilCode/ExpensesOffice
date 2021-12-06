using System;

namespace OfficeExpenses
{
    class Department
    {
        // название отдела
        public string name { get; private set; }
        // кол-во сотрудников
        public int count { get; private set; }
        // конструктор с 2-мя параметрами
        public Department(string name, int count)
        {
            this.name = name;
            this.count = count;
        }
        // конструктор для чтения из файла
        public Department(string[] arr)
        {
            this.name = arr[0];
            this.count = Convert.ToInt32(arr[1]);
        }
        // метод AddEmployee - увеличение кол-ва сотрудников в отделе
        public void AddEmployee()
        {
            count++;
        }
        // метод Print() для печати на экран информации об отделе
        public void Print()
        {
            Console.WriteLine($"Название отдела: {name}\n" +
                $"Количество сотрудников: {count}");
        }
        // переопределение метода ToString() для записи в файл
        public override string ToString()
        {
            return $"{name}%{count}";
        }
        public static bool operator ==(Department dep1, Department dep2)
        {
            return (dep1.name == dep2.name && dep1.count == dep2.count);
        }
        public static bool operator !=(Department dep1, Department dep2)
        {
            return !(dep1 == dep2);
        }
    }
}
