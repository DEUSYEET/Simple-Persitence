using System;
using System.IO;

namespace SimplePersistence
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var files = Directory.GetFiles("C:\\Users\\Wesley Monk\\Documents\\Quarter 5 Classes\\Databases 2\\People Records\\people\\simple");
            var files = Directory.GetFiles("C:\\Users\\drago\\Downloads\\People Records\\people\\simple");
            foreach (var f in files)
            {
                //PrintPeopleDetails(f);
                PrintEmployees(f);
            }
        }




        public static void PrintPeopleDetails(string path)
        {
            StreamReader sr = File.OpenText(path);
            string s;
            while ((s = sr.ReadLine())!= null){
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

            //Console.WriteLine(text);
        }

        //FileIO file.readAllText

    }
}
