using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeExpenses
{
    class Employee
    {
        // отдел
        public Department department { get; private set; }
        // ФИО
        public string fullname { get; private set; }
        // конструктор
        public Employee (Department department, string fullname)
        {
            this.department = department;
            this.fullname = fullname;
        }
        // метод Print() для печати на экран информации о сотруднике
        public void Print()
        {
            Console.WriteLine("Отдел:");
            department.Print();
            Console.WriteLine($"Полное имя: {fullname}");
        }
    }
}
