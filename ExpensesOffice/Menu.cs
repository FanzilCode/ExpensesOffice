using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections;

namespace OfficeExpenses
{
    class Menu
    {
        // отчёты по расходам
        static List<ExpenseReport> reports = new List<ExpenseReport>();
        // сотрудники
        static List<Employee> employees = new List<Employee>();
        // виды расходов
        static List<ExpenseType> expenseTypes = new List<ExpenseType>();
        // отделы
        static List<Department> departments = new List<Department>();
        // нормы расходов
        static List<ExpenseNorm> normExpenses = new List<ExpenseNorm>();
        // месяцы и годы
        static List<Date> dates = new List<Date>();

        // сохранение в файл
        static void SaveToFile(string path)
        {
            string strings = "";
            foreach (var report in reports)
            {
                strings += report + "\n";
            }
            strings = strings.Trim();
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(strings);
            }
        }
        static void SaveToFile2(string path)
        {
            string strings = "";
            foreach (var norm in normExpenses)
            {
                strings += norm + "\n";
            }
            strings = strings.Trim();
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(strings);
            }
        }
        // чтение из файла
        static void ReadOnFile(string path)
        {
            string line;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    ExpenseType expenseType = new ExpenseType(line.Trim(), sr.ReadLine().Trim());
                    if (expenseTypes.IndexOf(expenseType) < 0)
                        expenseTypes.Add(expenseType);

                    Department department = new Department(sr.ReadLine().Split("%"));
                    if (departments.IndexOf(department) < 0)
                        departments.Add(department);

                    Employee employee = new Employee(department, sr.ReadLine().Trim());
                    if (employees.IndexOf(employee) < 0)
                        employees.Add(employee);

                    string[] arr = line.Split("%");
                    double cost = Convert.ToDouble(arr[0]);
                    DateTime date = Convert.ToDateTime(arr[1]);
                    Date d1 = new Date(date);
                    if (!dates.Contains(d1))
                    {
                        dates.Add(d1);
                        dates.Sort();
                    }

                    reports.Add(new ExpenseReport(expenseType, department, cost, date, employee));
                }
            }
        }
        // метод PrintEmployees() - печать списка сотрудников на экран
        static void PrintEmployees()
        {
            foreach (var emp in employees)
            {
                Console.WriteLine($"Индекс: {employees.IndexOf(emp)} ");
                emp.Print();
            }
        }
        // метод PrintDepartments() - печать списка отделов на экран
        static void PrintDepartments()
        {
            foreach (var dep in departments)
            {
                Console.WriteLine($"Индекс: {departments.IndexOf(dep)} ");
                dep.Print();
            }
        }
        // метод PrintExpenseTypes() - печать списка видов расходов на экран
        static void PrintExpenseTypes()
        {
            foreach (var ex in expenseTypes)
            {
                Console.WriteLine($"Индекс: {expenseTypes.IndexOf(ex)} ");
                ex.Print();
            }
        }
        // метод PrintCostRates() - печать норм расходов на экран
        static void PrintNormExpenses()
        {
            foreach (var rate in normExpenses)
            {
                Console.WriteLine($"Индекс: {normExpenses.IndexOf(rate)} ");
                rate.Print();
            }
        }
        // метод PrintNormExpenses(Department dep) - печать норм расходов по отделу на экран
        static void PrintNormExpenses(Department dep)
        {
            foreach (var date in dates)
            {
                date.Print();
                Console.WriteLine("\n\n");
                foreach (var norm in normExpenses)
                {
                    if (norm.IsAvailable(dep, date))
                        norm.Print();
                }
            }
        }
        // метод PrintReports() - печать списка отчётов по расходам
        static void PrintReports()
        {
            foreach (var rep in reports)
            {
                Console.WriteLine($"Индекс: {reports.IndexOf(rep)}");
                rep.Print();
            }
        }
        // метод PrintReports(Department dep) - печать списка расходов по отделу на экран
        static void PrintReports(Department dep)
        {
            foreach (var rep in reports)
            {
                if (rep.IsAvailable(dep))
                    rep.Print();
            }
        }
        // метод PrintReports(DateTime date1, DateTime date2) - печать списка расходов за определенный период на экран
        static void PrintReports(DateTime date1, DateTime date2)
        {
            foreach (var rep in reports)
            {
                if (rep.IsAvailable(date1, date2))
                    rep.Print();
            }
        }
        // метод AddEmployee() - добавить сотрудника в список
        static void AddEmployee(int index)
        {
            Console.WriteLine("\t\t\tДобавить сотрудника");
            Console.Write("Полное имя: ");
            string fullname = Console.ReadLine();
            Employee emp = new Employee(departments[index], fullname);
            Console.Clear();
            if (employees.IndexOf(emp) < 0)
            {
                employees.Add(emp);
                Console.WriteLine("Сотрудник добавлен в список");
                if (departments[index].count < employees.Count(x => x.department == departments[index]))
                    departments[index].AddEmployee();
            }
            else
                Console.WriteLine("Сотрудник уже в списке.");
            PrintEmployees();
        }
        // метод AddDepartment() - добавить отдел в список
        static void AddDepartment()
        {
            Console.WriteLine("\t\t\tДобавить отдел");
            Console.Write("Введите название отдела: ");
            string name = Console.ReadLine();
            Console.Write("Введите кол-во сотрудников: ");
            int count = Convert.ToInt32(Console.ReadLine());
            Department dep = new Department(name, count);
            if (departments.IndexOf(dep) < 0)
            {
                departments.Add(dep);
                Console.WriteLine("Отдел добавлен в список.");
                Console.WriteLine("Добавьте сотрудников отдела:");
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"Добавьте {i + 1}-го сотрудника отдела");
                    AddEmployee(departments.Count - 1);
                }
                FillNormExpenses(dep);
            }
            else
            {
                Console.WriteLine("Отдел уже есть в списке");
            }
        }
        // метод AddExpenseType() - добавить вид расхода
        static void AddExpenseType()
        {
            Console.WriteLine("\t\t\tДобавить вид расхода");

            Console.Write("Введите название: ");
            string type = Console.ReadLine();

            Console.Write("Описание: ");
            string description = Console.ReadLine();
            Console.Clear();

            ExpenseType expenseType = new ExpenseType(type, description);
            if (expenseTypes.IndexOf(expenseType) < 0)
            {
                expenseTypes.Add(expenseType);
                Console.WriteLine("Вид расхода добавлен в список.\n\n");
                FillNormExpenses(expenseType);
            }
            else
            {
                Console.WriteLine("Данный вид расхода уже есть в списке.\n\n");
            }
            PrintExpenseTypes();
        }
        // метод AddReport() - добавить отчёт о расходе в список
        static void AddReport()
        {
            Console.WriteLine("\t\t\tДобавить отчёт о расходе.");
            Console.WriteLine("Выберите вид расхода(введите индекс):");
            PrintExpenseTypes();
            int indexType = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Выберите сотрудника(введите индекс):");
            int indexEmp = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите сумму расхода(в рублях): ");
            double cost = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите дату(в формате ДД.ММ.ГГГГ) расхода: ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            Date d1 = new Date(date);
            if (!dates.Contains(d1))
            {
                dates.Add(d1);
                dates.Sort();
            }

            ExpenseReport report = new ExpenseReport(expenseTypes[indexType], employees[indexEmp].department, cost, date, employees[indexEmp]);
            Console.Clear();
            if(reports.IndexOf(report) < 0)
            {
                reports.Add(report);
                Console.WriteLine("Отчёт о расходе добавлен в список.");
            }
            else
            {
                Console.WriteLine("Данный отчёт уже в списке.");
            }
            PrintReports();
        }
        // перегрузки метода FillNormExpenses() - заполнить список норм расходов
        static void FillNormExpenses()
        {
            dates.Sort();
            foreach (var ex in expenseTypes)
            {
                foreach (var dep in departments)
                {
                    foreach (var date in dates)
                    {
                        if (!normExpenses.Any(x => x.expenseType == ex && x.department == dep && x.date == date))
                        {
                            ex.Print();
                            dep.Print();
                            date.Print();
                            Console.WriteLine("Введите лимит расходов(в рублях) для текущих вида расхода, отдела офиса, месяца и года:");
                            double limit = Convert.ToDouble(Console.ReadLine());
                            normExpenses.Add(new ExpenseNorm(ex, dep, date, limit));
                        }
                    }
                }
            }
        }
        static void FillNormExpenses(ExpenseType ex)
        {
            dates.Sort();
            foreach (var dep in departments)
            {
                foreach (var date in dates)
                {
                    if (!normExpenses.Any(x => x.expenseType == ex && x.department == dep && x.date == date))
                    {
                        ex.Print();
                        dep.Print();
                        date.Print();
                        Console.WriteLine("Введите лимит расходов(в рублях) для текущих вида расхода, отдела офиса, месяца и года:");
                        double limit = Convert.ToDouble(Console.ReadLine());
                        normExpenses.Add(new ExpenseNorm(ex, dep, date, limit));
                    }
                }
            }

        }
        static void FillNormExpenses(Department dep)
        {
            dates.Sort();
            foreach (var ex in expenseTypes)
            {
                foreach (var date in dates)
                {
                    if (!normExpenses.Any(x => x.expenseType == ex && x.department == dep && x.date == date))
                    {
                        ex.Print();
                        dep.Print();
                        date.Print();
                        Console.WriteLine("Введите лимит расходов(в рублях) для текущих вида расхода, отдела офиса, месяца и года:");
                        double limit = Convert.ToDouble(Console.ReadLine());
                        normExpenses.Add(new ExpenseNorm(ex, dep, date, limit));
                    }
                }

            }
        }
        static void FillNormExpenses(Date date)
        {
            dates.Sort();
            foreach (var ex in expenseTypes)
            {
                foreach (var dep in departments)
                {
                    if (!normExpenses.Any(x => x.expenseType == ex && x.department == dep && x.date == date))
                    {
                        ex.Print();
                        dep.Print();
                        date.Print();
                        Console.WriteLine("Введите лимит расходов(в рублях) для текущих вида расхода, отдела офиса, месяца и года:");
                        double limit = Convert.ToDouble(Console.ReadLine());
                        normExpenses.Add(new ExpenseNorm(ex, dep, date, limit));
                    }
                }
            }
        }
        static void FillNormExpenses(string path)
        {
            string line;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    ExpenseType expenseType = new ExpenseType(line.Trim(), sr.ReadLine().Trim());
                    Department department = new Department(sr.ReadLine().Split("%"));
                    Date date = new Date(sr.ReadLine().Split("%"));
                    double limit = Convert.ToDouble(sr.ReadLine());
                    if (!normExpenses.Any(x => x.expenseType == expenseType && x.department == department && x.date == date))
                        normExpenses.Add(new ExpenseNorm(expenseType, department, date, limit));
                }
            }
        }
        // метод GetRemains() - остаток по норме
        static double GetRemains(ExpenseNorm expenseNorm)
        {
            return expenseNorm.GetRemains(reports.Where(x => x.department == expenseNorm.department && x.IsAvailable(expenseNorm.date)
            && x.expenseType == expenseNorm.expenseType).Sum(y => y.cost));
        }
        static void Main(string[] args)
        {
            string path = @"Reports.txt";
            ReadOnFile(path);
            string path2 = @"Norms.txt";
            FillNormExpenses(path2);
            int choise = 1;
            while(choise >= 1 && choise <= 3)
            {
                Console.WriteLine("\t\t\tГлавная\nВыберите:");
                Console.WriteLine("1) Нормы расходов\n2) Расходы\n3) Справочники\n4) Выйти из приложения");
                choise = Convert.ToInt32(Console.ReadLine());
                switch(choise)
                {
                    case 1:
                        {
                            Console.WriteLine("\t\t\tНормы расходов\nВыберите:");
                            Console.WriteLine("1) Получить список норм расходов\n" +
                                "2) Получить список норм расходов по отделу\n3) На главную");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            switch(choise2)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("\t\t\tПолучить список норм расходов");
                                        PrintNormExpenses();
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("\t\t\tПолучить список норм расходов по отделу");
                                        Console.WriteLine("Выберите отдел(введите индекс):");
                                        PrintDepartments();
                                        int index = Convert.ToInt32(Console.ReadLine());
                                        PrintNormExpenses(departments[index]);
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("\t\t\tРасходы\nВыберите:");
                            Console.WriteLine("1) Получить список отчётов о расходах\n2) Получить список отчётов о расходах по отделу\n" +
                                "3) Получить список отчётов о расходах за определённый период\n" +
                                "4) Добавить отчёт о расходе\n5) На главную");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            switch(choise2)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("\t\t\tПолучить список отчётов о расходах");
                                        PrintReports();
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("\t\t\tПолучить список отчётов о расходах по отделу");
                                        Console.WriteLine("Выберите отдел(введите индекс):");
                                        PrintDepartments();
                                        int index = Convert.ToInt32(Console.ReadLine());
                                        PrintReports(departments[index]);
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.WriteLine("\t\t\tПолучить список отчётов о расходах за определённый период");

                                        Console.Write("Введите дату, с которой нужно начать отсчет: ");
                                        DateTime date1 = Convert.ToDateTime(Console.ReadLine());
                                        Console.Write("Введите дату, до которой нужно вести отсчет: ");
                                        DateTime date2 = Convert.ToDateTime(Console.ReadLine());

                                        PrintReports(date1, date2);

                                        break;
                                    }
                                case 4:
                                    {
                                        Console.WriteLine("\t\t\tДобавить отчёт о расходе");
                                        AddReport();
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("\t\t\tСправочники\nВыберите:");
                            Console.WriteLine("1) Виды расходов\n2) Отделы\n3) Сотрудники\n4) Месяц и год\n5) На главную");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            switch(choise2)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("\t\t\tВиды расходов");
                                        PrintExpenseTypes();
                                        Console.WriteLine("Хотите добавить новый вид расхода?\n1) Да\t2) Нет");
                                        int choise3 = Convert.ToInt32(Console.ReadLine());
                                        if (choise3 == 1)
                                            AddExpenseType();
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("\t\t\tОтделы");
                                        PrintDepartments();
                                        Console.WriteLine("Хотите добавить новый отдел?\n1) Да\t2) Нет");
                                        int choise3 = Convert.ToInt32(Console.ReadLine());
                                        if (choise3 == 1)
                                            AddDepartment();
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.WriteLine("\t\t\tСотрудники");
                                        PrintEmployees();
                                        Console.WriteLine("Хотите добавить нового сотрудника?\n1) Да\t2) Нет");
                                        int choise3 = Convert.ToInt32(Console.ReadLine());
                                        if (choise3 == 1)
                                        {
                                            Console.WriteLine("Выберите отдел сотрудника(введите индекс):");
                                            PrintDepartments();
                                            AddEmployee(Convert.ToInt32(Console.ReadLine()));
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        Console.WriteLine("\t\t\tМесяц и год");
                                        dates.Sort();
                                        foreach (var date in dates)
                                            date.Print();
                                        Console.ReadKey();
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            SaveToFile(path);
        }
    }
}