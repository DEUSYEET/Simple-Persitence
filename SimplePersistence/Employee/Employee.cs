using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePersistence.Employee
{
    class Employee
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int hireYear { get; set; }



        public override string ToString()
        {
            return $"ID: {id} | {lastName}, {firstName}| Hired in {hireYear}";
        }
    }
}
