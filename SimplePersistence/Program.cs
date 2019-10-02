using System;
using System.IO;

namespace SimplePersistence
{
    class Program
    {
        static void Main(string[] args)
        {
           var files = Directory.GetFiles("C:\\Users\\drago\\Downloads\\People Records\\people\\simple");
            foreach(var f in files)
            {
                PrintPeopleDetails(f);
            }
        }




        static void PrintPeopleDetails(string path)
        {
            StreamReader sr = File.OpenText(path);
            string s;
            while ((s = sr.ReadLine())!= null){
                Console.WriteLine(s);
            }

        }



    }
}
