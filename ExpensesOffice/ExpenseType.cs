using System;

namespace OfficeExpenses
{
    class ExpenseType
    {
        // название
        public string type { get; private set; }
        // описание
        public string description { get; private set; }
        // конструктор
        public ExpenseType(string type, string description)
        {
            this.type = type;
            this.description = description;
        }
        // метод Print() для печати на экран информации о расходе
        public void Print()
        {
            Console.WriteLine($"Вид расхода: {type}\n" +
                $"Описание: {description}");
        }

        // переопределение метода ToString() для записи в файл
        public override string ToString()
        {
            return $"{type}\n{description}";
        }
        public static bool operator ==(ExpenseType ex1, ExpenseType ex2)
        {
            return (ex1.type == ex2.type && ex1.description == ex2.description);
        }
        public static bool operator !=(ExpenseType ex1, ExpenseType ex2)
        {
            return !(ex1 == ex2);
        }
    }
}
