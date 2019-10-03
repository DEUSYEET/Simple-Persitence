using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePersistence.Employee
{
    public class LargeEmployee : Employee
    {
        public byte[] Data { get; set; }

        public LargeEmployee(int id, string firstName, string lastName, int hireYear, byte[] data)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.HireYear = hireYear;
            this.Data = data;
        }

    }
}
