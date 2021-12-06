using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OfficeExpenses
{
    class ExpenseReport
    {
        // вид расхода
        public ExpenseType expenseType { get; private set; }
        // отдел
        public Department department { get; private set; }
        // сумма
        public double cost { get; private set; }
        // дата
        public DateTime date { get; private set; }
        // сотрудник
        public Employee employee { get; private set; }

        // конструктор
        public ExpenseReport(ExpenseType expenseType, Department department, double cost, DateTime date, Employee employee)
        {
            this.expenseType = expenseType;
            this.department = department;
            this.cost = cost;
            this.date = date;
            this.employee = employee;
        }
        // метод Print() для печати информации о расходе на экран
        public void Print()
        {
            Console.WriteLine("Вид расхода:");
            expenseType.Print();

            Console.WriteLine("Отдел:");
            department.Print();

            Console.WriteLine("Сотрудник:");
            employee.Print();

            Console.WriteLine($"Сумма расхода: {cost} руб.");
            Console.WriteLine($"Дата: {date.ToShortDateString()}");
        }
        // переопределение метода ToString() для записи в файл
        public override string ToString()
        {
            return $"{expenseType}\n" +
                $"{department}\n" +
                $"{employee.fullname}" +
                $"{cost}%{date.ToShortDateString()}";
        }
        public static bool operator ==(ExpenseReport r1, ExpenseReport r2)
        {
            return (r1.expenseType == r2.expenseType && r1.department == r2.department && r1.date == r2.date && r1.employee.fullname == r2.employee.fullname);
        }
        public static bool operator !=(ExpenseReport r1, ExpenseReport r2)
        {
            return !(r1 == r2);
        }
        public bool IsAvailable(Date date)
        {
            return (date.month == this.date.Month && date.year == this.date.Year) ;
        }
        public bool IsAvailable(Department dep)
        {
            return (department == dep);
        }
        public bool IsAvailable(DateTime date1, DateTime date2)
        {
            return (date1 <= date && date2 >= date);
        }
    }
}
