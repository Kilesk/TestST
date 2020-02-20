using System;
using TestST.Services;

namespace TestST
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceDB service = new ServiceDB();
                string menuItem;
                do
                {
                    Console.WriteLine("Введите название команды:");
                    Console.WriteLine("1) 'class'      - Показать классы");
                    Console.WriteLine("2) 'student'    - Показать учеников");
                    Console.WriteLine("3) 'addClass'   - Добавить класс");
                    Console.WriteLine("4) 'addStudent' - Добавить ученика");
                    Console.WriteLine("5) 'delClass'   - Удалить класс");
                    Console.WriteLine("6) 'delStudent' - Удалить ученика");
                    Console.WriteLine("7) 'show'       - Показать учеников класс");
                    Console.WriteLine("8) 'add'        - Добавить ученика в класс");
                    Console.WriteLine("9) 'del'        - Исключить ученика из класса");
                    Console.WriteLine("10) 'exit'      - Выйти из программы");
                    Console.WriteLine("Ваше решение: ");

                    menuItem = Console.ReadLine();
                    if (!String.IsNullOrEmpty(menuItem))
                    {
                        switch (menuItem)
                        {
                            case "class":
                                service.ShowClasses();
                                break;
                            case "student":
                                service.ShowStudents();
                                break;
                            case "addClass":
                                service.AddClass();
                                break;
                            case "addStudent":
                                service.AddStudent();
                                break;
                            case "delClass":
                                service.DelClass();
                                break;
                            case "delStudent":
                                service.DelStudent();
                                break;
                            case "show":
                                service.ShowStudenOfClass();
                                break;
                            case "add":
                                service.AddStudentToClass();
                                break;
                            case "del":
                                service.DelStudentFromClass();
                                break;
                            case "exit":
                                Console.WriteLine("Вы решили выйти");
                                break;
                            default:
                                Console.WriteLine("Вы что-то другое ввели...");
                                break;
                        }
                        Console.Write("\n\n\t\t\tНажмите любую клавишу...");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Вы ничего не ввели...");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                while (menuItem != "Exit");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

    }
}
