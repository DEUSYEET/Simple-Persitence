using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SimplePersistence.Employee
{
    //[DataContract]
    [Serializable]
    public class Employee
    {
        //[DataMember]
        public int Id { get; set; }
        //[DataMember]
        public string FirstName { get; set; }
        //[DataMember]
        public string LastName { get; set; }
        //[DataMember]
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
