using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SimplePersistence
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var files = Directory.GetFiles("C:\\Users\\Wesley Monk\\Documents\\Quarter 5 Classes\\Databases 2\\People Records\\people\\simple");
            //foreach (var f in files)
            //{
            //    PrintPeopleDetails(f);
            //    PrintEmployees(f);
            //}
            //AddEmployee(10001, "Wesley", "Monk", 2000);
            //DeleteEmployee(10001);
            UpdateEmployee(10001, "Jacob", "Monk", 1996);
        }

        public static void PrintPeopleDetails(string path)
        {
            StreamReader sr = File.OpenText(path);
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                Console.WriteLine(s);
            }
        }

        public static void PrintEmployees(string path)
        {
            string text = File.ReadAllText(path);
            string[] words = text.Split(',');
            Int32.TryParse(words[0], out int id);
            Int32.TryParse(words[3], out int year);

            Employee.Employee emp = new Employee.Employee(id, words[1], words[2], year);
            Console.WriteLine(emp.ToString());
        }

        public static void AddEmployee(int id, string firstName, string lastName, int hireDate)
        {
            string fileLocation = "C:\\Users\\Wesley Monk\\Documents\\Quarter 5 Classes\\Databases 2\\People Records\\people\\long";
            var files = Directory.GetFiles(fileLocation);
            bool isThere = false;
            foreach (var f in files)
            {
                if (Path.GetFileName(f) == id + ".txt")
                {
                    Console.WriteLine("File already exists. Cannot overwrite");
                    isThere = true;
                }
            }

            if (!isThere)
            {
                System.IO.File.WriteAllText(fileLocation + $"\\{id}.txt", $"{id}, {firstName}, {lastName}, {hireDate}");
                Console.WriteLine("File successfully created");
            }
        }

        public static void DeleteEmployee(int id)
        {
            string fileLocation = "C:\\Users\\Wesley Monk\\Documents\\Quarter 5 Classes\\Databases 2\\People Records\\people\\long";
            var files = Directory.GetFiles(fileLocation);
            bool isThere = false;
            foreach (var f in files)
            {
                if (Path.GetFileName(f) == id + ".txt")
                {
                    //Console.WriteLine("File already exists. Cannot overwrite");
                    File.Delete(f);
                    isThere = true;
                }
            }

            if (!isThere)
            {
                Console.WriteLine("No Employee Associated with ID: " + id);
            }
            else
            {
                Console.WriteLine($"Employee {id} has been deleted");
            }
        }

        public static void UpdateEmployee(int id, string firstName, string lastName, int hireDate)
        {
            string fileLocation = "C:\\Users\\Wesley Monk\\Documents\\Quarter 5 Classes\\Databases 2\\People Records\\people\\long";
            var files = Directory.GetFiles(fileLocation);
            bool isThere = false;
            foreach (var f in files)
            {
                if (Path.GetFileName(f) == id + ".txt")
                {
                    File.Delete(f);
                    System.IO.File.WriteAllText(fileLocation + $"\\{id}.txt", $"{id}, {firstName}, {lastName}, {hireDate}");
                    isThere = true;
                }
            }

            if (!isThere)
            {
                Console.WriteLine("No Employee Associated with ID: " + id);
            }
            else
            {
                Console.WriteLine($"Employee {id} has been updated");
            }
        }
    }
}
