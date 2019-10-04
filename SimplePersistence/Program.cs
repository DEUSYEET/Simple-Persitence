using CSC160_ConsoleMenu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SimplePersistence
{
    public class Program
    {
        static string fileLocation = "C:\\Users\\Wesley Monk\\Documents\\Quarter 5 Classes\\Databases 2\\People Records\\people\\long";
        static string fileLocationSerial = "C:\\Users\\Wesley Monk\\Documents\\Quarter 5 Classes\\Databases 2\\People Records\\people\\long serialized";


        //static string fileLocation = "C:\\Users\\drago\\Downloads\\People Records\\people\\long";
        //static string fileLocationSerial = "C:\\Users\\drago\\Downloads\\People Records\\people\\longserialized";


        public static void Main(string[] args)
        {
            int choice;
            do
            {
                String[] menu = { "Add Employee", "Delete Employee", "Update Employee", "Serialize All Employees", "Find serialized employee", "Find Employee By ID", "Find Employee by Last Name", "Find All Employees with Same Last Name", "Print Serialized details", "Print all employees"  };
                choice = CIO.PromptForMenuSelection(menu, true);

                switch (choice)
                {
                    case 1:
                        AddEmployee(CIO.PromptForInt("Enter ID: ", 1, Int32.MaxValue), CIO.PromptForInput("Enter First Name: ", false), CIO.PromptForInput("Enter Last Name: ", false), CIO.PromptForInt("Enter Hire Date: ", 1, Int32.MaxValue));
                        break;
                    case 2:
                        DeleteEmployee(CIO.PromptForInt("Enter Employee ID: ", 1, Int32.MaxValue));
                        break;
                    case 3:
                        UpdateEmployee(CIO.PromptForInt("Enter ID: ", 1, Int32.MaxValue), CIO.PromptForInput("Enter First Name: ", false), CIO.PromptForInput("Enter Last Name: ", false), CIO.PromptForInt("Enter Hire Date: ", 1, Int32.MaxValue));
                        break;
                    case 4:
                        SerializeAllEmployees();
                        break;
                    case 5:
                        Console.WriteLine(GetSerializedEmployee(CIO.PromptForInt("Enter Employee ID: ", 1, Int32.MaxValue)).ToString());
                        break;
                    case 6:
                        Console.WriteLine(FindEmployeeById(CIO.PromptForInt("Enter Employee ID: ", 1, Int32.MaxValue)).ToString());
                        break;
                    case 7:
                        Console.WriteLine(FindEmployeeByLastName(CIO.PromptForInput("Enter Last Name: ", false)).ToString());
                        break;
                    case 8:
                        string lastName = CIO.PromptForInput("Enter Last Name: ", false);
                        foreach (var f in FindAllEmployeesByLastName(lastName))
                        {
                            Console.WriteLine(f.ToString());
                        }
                        break;


                    case 9:
                        PrintSerializedDetails(fileLocationSerial);
                        break;
                    case 10:
                        printAllEmployees();
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
        public static void SerializeAllEmployees()
        {
            //DataContractSerializer ser = new DataContractSerializer(typeof(Employee.Employee));
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            var files = Directory.GetFiles(fileLocation);
            foreach (var f in files)
            {
                string text = File.ReadAllText(f);
                string[] words = text.Split(',');
                Int32.TryParse(words[0], out int id);
                Int32.TryParse(words[3], out int year);

                Employee.Employee emp = new Employee.Employee(id, words[1], words[2], year);
                Stream s = File.Open(fileLocationSerial + $"\\{id}.ser", FileMode.Create);
                binaryFormatter.Serialize(s, emp);
                Console.WriteLine($"Serialized employee ID: {id}");
                s.Close();



            }

        }

        public static Employee.Employee GetSerializedEmployee(int id)
        {
            Stream s = File.Open(fileLocationSerial + $"\\{id}.ser", FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Employee.Employee e = (Employee.Employee)binaryFormatter.Deserialize(s);
            s.Close();

            return e;
        }

  public static Employee.Employee FindEmployeeById(int id)
        {
            Employee.Employee emp = new Employee.Employee(0, "N/A", "N/A", 0);
            var files = Directory.GetFiles(fileLocation);
            foreach (var f in files)
            {
                if (Path.GetFileName(f) == id + ".txt")
                {
                    string text = File.ReadAllText(f);
                    string[] words = text.Split(',');
                    Int32.TryParse(words[3], out int year);

                    emp = new Employee.Employee(id, words[1], words[2], year);
                }
            }
            return emp;
        }

        public static Employee.Employee FindEmployeeByLastName(string lastName)
        {
            Employee.Employee emp = new Employee.Employee(0, "N/A", "N/A", 0);
            var files = Directory.GetFiles(fileLocation);
            foreach (var f in files)
            {
                string text = File.ReadAllText(f);
                string[] words = text.Split(',');
                if (lastName.ToLower().Equals(words[2].ToLower().Trim()))
                {
                    Int32.TryParse(words[3], out int id);
                    Int32.TryParse(words[3], out int year);
                    emp = new Employee.Employee(id, words[1], words[2], year);
                    break;
                }
            }
            return emp;
        }

        public static List<Employee.Employee> FindAllEmployeesByLastName(string lastName)
        {
            List<Employee.Employee> empList = new List<Employee.Employee>();

            var files = Directory.GetFiles(fileLocation);
            foreach (var f in files)
            {
                string text = File.ReadAllText(f);
                string[] words = text.Split(',');
                if (lastName.ToLower().Equals(words[2].ToLower().Trim()))
                {
                    Int32.TryParse(words[0], out int id);
                    Int32.TryParse(words[3], out int year);
                    Employee.Employee emp = new Employee.Employee(id, words[1], words[2], year);
                    empList.Add(emp);
                }
            }
            return empList;
        }

        public static void PrintSerializedDetails(string path)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            var files = Directory.GetFiles(path);
            foreach (var f in files)
            {
                Stream s = File.Open(f, FileMode.Open);
                Employee.Employee e = (Employee.Employee)binaryFormatter.Deserialize(s);
                Console.WriteLine(e.ToString());
            }
        }

        public static Dictionary<int, Employee.Employee> getAllEmployees(string path)
        {
            Dictionary<int, Employee.Employee> employees = new Dictionary<int, Employee.Employee>();


            BinaryFormatter binaryFormatter = new BinaryFormatter();
            var files = Directory.GetFiles(path);
            foreach (var f in files)
            {
                Stream s = File.Open(f, FileMode.Open);
                Employee.Employee e = (Employee.Employee)binaryFormatter.Deserialize(s);
                employees.Add(e.Id, e);
            }


            return employees;
        }


        public static void printAllEmployees()
        {
            foreach(var e in getAllEmployees(fileLocationSerial))
            {
                Console.WriteLine(e.Value.ToString());
            }
        }

    }
}
