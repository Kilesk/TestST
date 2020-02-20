using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestST.Model;

namespace TestST.Services
{
    class ServiceDB
    {
        public ServiceDB()
        {

        }
        public void ShowClasses()
        {
            using (TestDBEntities context = new TestDBEntities())
            {
                context.Classes.Load();
                var classes = context.Classes.Local
                                             .Where(s => s.IsDelete == false)
                                             .Select(s => s).ToList();
                if (classes.Count > 0)
                {
                    foreach (var item in classes)
                    {
                        Console.WriteLine("{0} - Класс {1}{2} ", item.ClassId, item.Number, item.Letter);
                    }
                }
                else
                {
                    Console.WriteLine("Классов нет.");
                }
            }
        }
        public void ShowStudents()
        {
            using (TestDBEntities context = new TestDBEntities())
            {
                context.Students.Load();
                var students = context.Students.Local
                                             .Where(s => s.IsDelete == false)
                                             .Select(s => s).ToList();
                ShowStudents(students);
            }
        }
        public void AddClass()
        {
            Console.WriteLine("Добавление класса");
            Console.WriteLine("Введите номер класса: ");

            int number = InsertNumber();
            
            Console.WriteLine("Введите букву класса: ");
            string letter = Console.ReadLine();

            AddClassToDB(number, letter);
            Console.WriteLine("Класс добавлен.");
        }
        public void AddStudent()
        {
            Console.WriteLine("Добавление ученика");
            Console.WriteLine("Введите имя: ");
            string name = Console.ReadLine();
            Console.WriteLine("Введите фамилию: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Введите возраст: ");
            int age = InsertNumber();
            AddStudentToDB(name, surname, age);
            Console.WriteLine("Ученик добавлен.");
        }
        public void DelClass()
        {
            Console.WriteLine("Удаление класса");
            Console.WriteLine("Укажите id класса: ");
            int id = InsertNumber();
            if (!IsHaveStudentsInClass(id))
            {
                using (TestDBEntities context = new TestDBEntities())
                {

                    Classes selectClass = context.Classes
                                                 .Where(c => c.ClassId == id && c.IsDelete == false)
                                                 .FirstOrDefault();
                    selectClass.IsDelete = true;
                    context.SaveChanges();
                }
                Console.WriteLine("Класс удален.");
            }
            else
            {
                Console.WriteLine("В класе есть ученики.");
            }
        }
        public void DelStudent()
        {
            Console.WriteLine("Удаление Ученика");
            Console.WriteLine("Укажите id ученика: ");
            int id = InsertNumber();
            using (TestDBEntities context = new TestDBEntities())
            {

                Students selectStudent = context.Students
                                       .Where(s => s.StudentsId == id && s.IsDelete == false)
                                       .FirstOrDefault();
                selectStudent.IsDelete = true;
                context.SaveChanges();
            }
            Console.WriteLine("Ученик удален.");
        }
        private void AddClassToDB(int number, string letter)
        {
            using (TestDBEntities context = new TestDBEntities())
            {
                Classes newClass = new Classes
                {
                    Number = number,
                    Letter = letter
                };
                context.Classes.Add(newClass);
                context.SaveChanges();
            }
        }
        private void AddStudentToDB(string name, string surname, int age)
        {
            using (TestDBEntities context = new TestDBEntities())
            {
                Students newStudent = new Students
                {
                    Name = name,
                    Surname = surname,
                    Age = age
                };
                context.Students.Add(newStudent);
                context.SaveChanges();
            }
        }
        private bool IsHaveStudentsInClass(int idClass)
        {
            using (TestDBEntities context = new TestDBEntities())
            {
                var students = context.Students
                                      .Where(s => s.ClassId == idClass && s.IsDelete == false)
                                      .Select(s => s).ToList();


                return students.Count > 0 ? true : false;
            }
        }
        private bool IsHaveStudentInClass(int idStudent, int idClass)
        {
            using (TestDBEntities context = new TestDBEntities())
            {
                var students = context.Students
                                      .Where(s => s.ClassId == idClass 
                                               && s.StudentsId == idStudent 
                                               && s.IsDelete == false)
                                      .FirstOrDefault();

                return students != null ? true : false;
            }
        }
        private bool IsHaveStudents(int idStudent)
        {
            using (TestDBEntities context = new TestDBEntities())
            {
                var students = context.Students
                                      .Where(s => s.StudentsId == idStudent && s.IsDelete == false)
                                      .FirstOrDefault();
                return students != null ? true : false;
            }
        }
        private bool IsHaveClass(int idClass)
        {
            using (TestDBEntities context = new TestDBEntities())
            {
                var classes = context.Classes
                                      .Where(c => c.ClassId == idClass && c.IsDelete == false)
                                      .FirstOrDefault();
                return classes != null ? true : false;
            }
        }
        public void AddStudentToClass()
        {
            Console.WriteLine("Включение ученика в класс");

            Console.WriteLine("Введите id ученика: ");
            int idStudent = InsertNumber();

            Console.WriteLine("Введите id класса: ");
            int idClass = InsertNumber();

            if (IsHaveStudents(idStudent) && IsHaveClass(idClass))
            {
                using (TestDBEntities context = new TestDBEntities())
                {
                    Students selectStudent = context.Students
                                                    .Where(s => s.StudentsId == idStudent && s.IsDelete == false)
                                                    .FirstOrDefault();
                    selectStudent.ClassId = idClass;
                    context.SaveChanges();
                }
                Console.WriteLine("Ученик добавлен в класс");
            }
            else
            {
                Console.WriteLine("Не существует указанного ученика или класса.");
            }


        }
        public void DelStudentFromClass()
        {
            Console.WriteLine("Исключение ученика из класса");

            Console.WriteLine("Введите id ученика: ");
            int idStudent = InsertNumber();

            Console.WriteLine("Введите id класса: ");
            int idClass = InsertNumber();

            if (IsHaveStudents(idStudent) && IsHaveClass(idClass))
            {
                if (IsHaveStudentInClass(idStudent, idClass))
                {
                    using (TestDBEntities context = new TestDBEntities())
                    {
                        Students selectStudent = context.Students
                                                        .Where(s => s.StudentsId == idStudent && s.IsDelete == false)
                                                        .FirstOrDefault();
                        selectStudent.ClassId = null;
                        context.SaveChanges();
                    }
                    Console.WriteLine("Ученик исключен из класса");
                }
            }
            else
            {
                Console.WriteLine("Не существует указанного ученика или класса.");
            }

        }
        public void ShowStudenOfClass()
        {
            Console.WriteLine("Введите id класса: ");
            int idClass = InsertNumber();
            if (IsHaveClass(idClass))
            {
                using (TestDBEntities context = new TestDBEntities())
                {
                    context.Students.Load();
                    var students = context.Students.Local
                                                 .Where(s => s.IsDelete == false && s.ClassId == idClass)
                                                 .Select(s => s).ToList();
                    ShowStudents(students);
                }
            }
            else
            {
                Console.WriteLine("Не существует класса.");
            }
        }
        private static void ShowStudents(List<Students> students)
        {
            if (students.Count > 0)
            {
                foreach (var item in students)
                {
                    Console.WriteLine("{0, 3} - Ученик: {1} {2} \t Возраст: {3} ", item.StudentsId, item.Surname, item.Name, item.Age);
                }
            }
            else
            {
                Console.WriteLine("Учеников нет.");
            }
        }
        private static int InsertNumber()
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Ошибка ввода! Введите целое число");
            }
            return number;
        }
    }
}
