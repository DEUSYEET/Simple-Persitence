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
            //var files = Directory.GetFiles("C:\\Users\\Wesley Monk\\Documents\\Quarter 5 Classes\\Databases 2\\People Records\\people\\simple");
            var files = Directory.GetFiles("C:\\Users\\drago\\Downloads\\People Records\\people\\simple");
            //foreach (var f in files)
            //{
            //    PrintPeopleDetails(f);
            //    PrintEmployees(f);
            //}
            SerializeAllEmployees();

            Console.WriteLine(GetSerializedEmployee(420).ToString());


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

            //Console.WriteLine(text);
        }

        //FileIO file.readAllText

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


    }
}
