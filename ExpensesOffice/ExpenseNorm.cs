using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeExpenses
{
    class ExpenseNorm
    {
        // вид расхода
        public ExpenseType expenseType { get; private set; }
        // отдел
        public Department department { get; private set; }
        // месяц и год
        public Date date { get; private set; }
        // лимит
        public double limit { get; private set; }
        // остаток
        public double remains { get; private set; }

        // конструктор
        public ExpenseNorm(ExpenseType expenseType, Department department, Date date, double limit)
        {
            this.expenseType = expenseType;
            this.department = department;
            this.date = date;
            this.limit = limit;
        }
        // метод Print() - печать информации о норме расхода на экран
        public void Print()
        {
            Console.WriteLine("Вид расхода:");
            expenseType.Print();

            Console.WriteLine("Отдел:");
            department.Print();

            Console.WriteLine($"Лимит расхода расхода: {limit} руб.");
            date.Print();
        }
        // переопределение метода ToString() для записи в файл
        public override string ToString()
        {
            return $"{expenseType}\n" +
                $"{department}\n" +
                $"{date}\n" +
                $"{limit}";
        }
        public static bool operator ==(ExpenseNorm norm1, ExpenseNorm norm2)
        {
            return (norm1.expenseType == norm2.expenseType && norm1.department == norm2.department && norm1.date == norm2.date);
        }
        public static bool operator !=(ExpenseNorm norm1, ExpenseNorm norm2)
        {
            return !(norm1 == norm2);
        }
        public bool IsAvailable(Department dep, Date date)
        {
            return (department == dep && this.date == date);
        }
        public double GetRemains(double sum)
        {
            remains = limit - sum;
            return remains;
        }
    }
}
