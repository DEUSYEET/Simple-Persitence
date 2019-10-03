using CSC160_ConsoleMenu;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SimplePersistence
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int choice;
            //var files = Directory.GetFiles("C:\\Users\\Wesley Monk\\Documents\\Quarter 5 Classes\\Databases 2\\People Records\\people\\simple");
            //foreach (var f in files)
            //{
            //    PrintPeopleDetails(f);
            //    PrintEmployees(f);
            //}
            do
            {
                String[] menu = { "Add Employee", "Delete Employee", "Update Employee", "Serialize All Employees", "Find serialized employee" };
                choice = CIO.PromptForMenuSelection(menu, true);

                switch (choice)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        SerializeAllEmployees();
                        break;
                    case 5:
                        Console.WriteLine(GetSerializedEmployee(CIO.PromptForInt("Enter Employee ID: ",1,10000)).ToString());
                        break;
                }




            } while (choice != 0);



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

        public static void SerializeAllEmployees()
        {
            //DataContractSerializer ser = new DataContractSerializer(typeof(Employee.Employee));
            BinaryFormatter binaryFormatter = new BinaryFormatter();



            //var files = Directory.GetFiles("C:\\Users\\Wesley Monk\\Documents\\Quarter 5 Classes\\Databases 2\\People Records\\people\\simple");
            var files = Directory.GetFiles("C:\\Users\\drago\\Downloads\\People Records\\people\\long");
            foreach (var f in files)
            {
                string text = File.ReadAllText(f);
                string[] words = text.Split(',');
                Int32.TryParse(words[0], out int id);
                Int32.TryParse(words[3], out int year);

                Employee.Employee emp = new Employee.Employee(id, words[1], words[2], year);


                Stream s = File.Open($"C:\\Users\\drago\\Downloads\\People Records\\people\\longserialized\\{id}.txt", FileMode.Create);

                //ser.WriteObject(s, emp);

                binaryFormatter.Serialize(s, emp);
                Console.WriteLine($"Serialized employee ID: {id}");
                s.Close();



            }

        }


        public static Employee.Employee GetSerializedEmployee(int id)
        {
            Stream s = File.Open($"C:\\Users\\drago\\Downloads\\People Records\\people\\longserialized\\{id}.txt", FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Employee.Employee e = (Employee.Employee)binaryFormatter.Deserialize(s);
            s.Close();

            return e;
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
