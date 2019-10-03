using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePersistence.Employee
{
    public class Employee
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int HireYear { get; set; }

        public Employee(int id, string firstName, string lastName, int hireYear)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.HireYear = hireYear;
        }

        public override string ToString()
        {
            return $"ID: {Id} | {LastName}, {FirstName}| Hired in {HireYear}";
        }
    }
}
